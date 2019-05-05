import React, { Component } from "react";
import "react-notifications/lib/notifications.css";
import "react-datepicker/dist/react-datepicker.css";
import DatePicker from "react-datepicker";
import "./NavMenu.css";
import "./style.css";
import "react-confirm-alert/src/react-confirm-alert.css";
import { staticData } from "./Context";

export class EditEvent extends Component {
    static displayName = EditEvent.name;
    constructor(props) {
        super(props);
        let typeList = staticData.eventTypes;
        this.state = {
            event: [],
            title: "",
            description: "",
            type: typeList,
            selectedType: null,
            redirect: false,
            startDate: new Date(),
            endDate: new Date(),
            loading: true,
            offset: new Date().getTimezoneOffset()
        }
        this.startPage = this.startPage.bind(this);
        this.startPage(this.props.match.params.id);
    }
    startPage(id) {
        fetch("api/event/edit/" + id)
            .then(response => {
                const json = response.json();
                console.log(json);
                return json;
            }).then(data => {
                console.log(data.startDate);
                console.log(this.state.event);
                let startMinutes = new Date(data.startDate).setMinutes(new Date(data.startDate).getMinutes() - this.state.offset);
                let endMinutes = new Date(data.endDate).setMinutes(new Date(data.endDate).getMinutes() - this.state.offset);
                data.startDate = new Date(startMinutes).toISOString();
                data.endDate = new Date(endMinutes).toISOString();
                console.log(data.startDate);
                this.setState({
                    event: data, loading: false, selectedType: data.type
                });
                console.log(this.state.event);
            });
    }
    handleChange(propertyName, e) {
        const event = this.state.event;
        if (propertyName === "startDate" || propertyName === "endDate") {
            event[propertyName] = e;
        } else {
            event[propertyName] = e.target.value;
        }
        if (propertyName === "type") {
            this.state.selectedType = e.target.value;
        }
        this.setState({ event: event });
    }
    handleClick(id) {
        let body = {
            Title: this.state.event.title,
            Description: this.state.event.description,
            Type: this.state.selectedType,
            StartDate: this.state.event.startDate,
            EndDate: this.state.event.endDate,
            Id: this.state.event.id
        }
        fetch("api/event/edit/" + id,
                {
                    method: "PUT",
                    headers: {
                        "Accept": "application/json",
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(body)
                }).then((response) => response.json())
            .then(data => {

                console.log(data);
                console.log(this.state.event);
                this.setState({ event: data, redirect: true });
                console.log(this.state.event);
            });
    }
    handleCancel() {
        this.props.history.push("/event/list");
    }
    renderRedirect() {
        if (this.state.redirect) {
            this.props.history.push({
                pathname: "/event/list",
                state: { actionMessage: "edited" }
            });
        }
    }
    renderEditForm(event) {
        return <div>
            <div className="form-group row">
                <label className=" control-label col-md-12">Title:</label>
                <div className="col-md-4">
                    <input className="form-control"
                        type="text"
                        value={event.title}
                        onChange={this.handleChange.bind(this, "title")} />
                </div>
            </div>
            <div className="form-group row">
                <label className=" control-label col-md-12">Description:</label>
                <div className="col-md-4">
                    <input className="form-control"
                        type="text"
                        value={event.description}
                        onChange={this.handleChange.bind(this, "description")} />
                </div>
            </div>
            <div className="form-group row">
                <label className=" control-label col-md-12">Type: </label>
                <div className="col-md-4">
                    <select className="form-control" value={this.state.selectedType} onChange={this.handleChange.bind(this, "type")} >
                        <option value="">-- Select type --</option>
                        {this.state.type.map(et =>
                            <option key={et.name} value={et.value}>{et.name}</option>
                        )}
                    </select>
                </div>
            </div >
            <div className="form-group row">
                <label className=" control-label col-md-12">Start date: </label>
                <div className="col-md-4">
                    <DatePicker className="form-control"
                        selected={event.startDate}
                        onChange={this.handleChange.bind(this, "startDate")}
                        showTimeSelect
                        timeFormat="HH:mm"
                        timeIntervals={15}
                        dateFormat="MMMM d, yyyy h:mm aa"
                        timeCaption="time"
                    />
                </div>
            </div>
            <div className="form-group row">
                <label className=" control-label col-md-12">End date: </label>
                <div className="col-md-4">
                    <DatePicker className="form-control"
                        selected={event.endDate}
                        onChange={this.handleChange.bind(this, "endDate")}
                        showTimeSelect
                        timeFormat="HH:mm"
                        timeIntervals={15}
                        dateFormat="MMMM d, yyyy h:mm aa"
                        timeCaption="time"
                    />
                </div>
            </div>
            <div className="form-group">
                <button className="btn btn-success" onClick={this.handleClick.bind(this)}>Save event</button>
                <button className="btn btn-danger" onClick={this.handleCancel.bind(this)}>Cancel</button>
            </div>
            {this.renderRedirect()}
        </div>;
    }
    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderEditForm(this.state.event);
        return (
            <div>
                <h1>Edit event</h1>
                <p>Edit the following fields.</p>

                {contents}
            </div>
        );
    }
}