import React, { Component } from "react";
import "react-notifications/lib/notifications.css";
import "react-datepicker/dist/react-datepicker.css";
import DatePicker from "react-datepicker";
import { staticData } from "./Context";

export class AddEvent extends Component {
    constructor(props) {
        let typeList = staticData.eventTypes;
        super(props);
        this.state = {
            title: "",
            description: "",
            type: typeList,
            selectedType: "",
            redirect: false,
            startDate: new Date(),
            endDate: new Date()
        }
        this.handleChange = this.handleChange.bind(this);
        this.handleChangeType = this.handleChangeType.bind(this);
        this.handleChangeDate = this.handleChangeDate.bind(this);
        this.handleChangeDateEnd = this.handleChangeDateEnd.bind(this);
    }
    handleChange(e) {
        this.setState({ title: e.target.value });
    }
    handleChangeDesc(e) {
        this.setState({ description: e.target.value });
    }
    handleChangeDate(date) {
        this.setState({
            startDate: date
        });
    }
    handleChangeDateEnd(date) {
        this.setState({
            endDate: date
        });
    }
    handleChangeType(e) {
        var newType = e.target.value;
        this.setState({ selectedType: newType });
        console.log(e.target.value);
    }
    handleClick() {
        let body = {
            Title: this.state.title,
            Description: this.state.description,
            Type: this.state.selectedType,
            StartDate: this.state.startDate,
            EndDate: this.state.endDate
        }
        //this.setState({ redirect: true });
        //setTimeout(() => {
        //    this.setState({ redirect: true })}, 2000);
        fetch("api/event/create",
            {
                method: "POST",
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(body)
            })//.then(NotificationManager.success('Success message', 'Event successfully added!', 3000))
	        .then(this.setState({ redirect: true }));

    }
    handleCancel() {
	    this.props.history.push("/event/list");
    }
    renderRedirect() {
        if (this.state.redirect) {
            this.props.history.push({
                pathname: "/event/list",
                state: { actionMessage: "added" }
            });
        }
    }
    renderCreateForm() {
        return <div>
            <div className="form-group row">
                <label className=" control-label col-md-12">Title:</label>
                <div className="col-md-4">
                    <input className="form-control"
                        type="text"
                        value={this.state.title}
                        onChange={this.handleChange}
                        placeholder="Write a title..." />
                </div>
            </div>
            <div className="form-group row">
                <label className=" control-label col-md-12">Description:</label>
                <div className="col-md-4">
                    <input className="form-control"
                        type="text"
                        value={this.state.description}
                        onChange={this.handleChangeDesc.bind(this)}
                        placeholder="Write a description..." />
                </div>
            </div>
            <div className="form-group row">
                <label className=" control-label col-md-12">Type: </label>
                <div className="col-md-4">
                    <select className="form-control" value={this.state.selectedType} onChange={this
                        .handleChangeType} >
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
                        selected={this.state.startDate}
                        onChange={this.handleChangeDate}
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
                        selected={this.state.endDate}
                        onChange={this.handleChangeDateEnd}
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
            : this.renderCreateForm();

        return (
            <div>
                <h1>Create event</h1>
                <p>Complete the following fields.</p>

                {contents}
            </div>
        );

    }
}