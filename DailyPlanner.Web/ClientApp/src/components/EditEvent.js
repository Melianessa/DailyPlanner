import React, { Component } from 'react';
import { RouteComponentProps, Router, Route } from 'react-router';
import { NavLink } from "reactstrap";
import { Link, Redirect } from 'react-router-dom';
import 'react-notifications/lib/notifications.css';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import "react-datepicker/dist/react-datepicker.css";
import DatePicker from 'react-datepicker';
import { EventList } from './EventList';
import "./NavMenu.css";
import "./style.css";
import { confirmAlert } from "react-confirm-alert"; // Import
import "react-confirm-alert/src/react-confirm-alert.css";

export class EditEvent extends Component {
    static displayName = EditEvent.name;
    constructor(props) {
        super(props);
        let typeList = [
            { name: "Meeting", value: 0 }, { name: "Reminder", value: 1 }, { name: "Event", value: 2 }, { name: "Task", value: 3 }];
        this.state = {
            event: [],
            title: '',
            description: '',
            type: typeList,
            selectedType: null,
            redirect: false,
            startDate: new Date(),
            endDate: new Date(),
            loading: true
        }
        //const id = this.props.match.params.id;
        //const id = this.props.match.params.id;
        this.handleChange = this.handleChange.bind(this);
        this.handleChangeType = this.handleChangeType.bind(this);
        this.handleChangeDate = this.handleChangeDate.bind(this);
        this.handleChangeDateEnd = this.handleChangeDateEnd.bind(this);
        this.startPage = this.startPage.bind(this);
        this.startPage(this.props.match.params.id);
    }
    startPage(id) {
        fetch('api/event/edit/' + id)
            .then(response => {
                const json = response.json();
                console.log(json);
                return json;
            }).then(data => {
                console.log(data);
                console.log(this.state.event);
                this.setState({
                    event: data, loading: false, selectedType: data.type
                });
                console.log(this.state.event);
            });
    }
    handleChange(e) {
        this.setState({ event: e.target.value });
    }
    handleChangeDesc(e) {
        this.setState({ description: e.target.value });
    }
    handleChangeDate(date) {
        this.setState({
	        event: date
        });
    }
    handleChangeDateEnd(date) {
        this.setState({
	        event: date
        });
    }
    handleChangeType(e) {
        var newType = e.target.value;
        this.setState({ selectedType: newType });
        console.log(e.target.value);
    }
    handleClick(id) {
        let body = {
            Title: this.state.title,
            Description: this.state.description,
            Type: this.state.selectedType,
            StartDate: this.state.startDate,
            EndDate: this.state.endDate
        }
        this.setState({ redirect: true });
        //setTimeout(() => {
        //    this.setState({ redirect: true })}, 2000);
        fetch('api/event/edit/' + id,
            {
                method: "PUT",
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(body)
            }).then((response) => response.json())
            .then((responseJson) => {
                this.props.history.push('/event/list');
            });
    }
    handleSubmit() {
        NotificationManager.success('Success message', 'Event successfully edited!', 1000000);
    }
    handleCancel() {
	    this.props.history.push('/event/list');
    } 
    renderRedirect() {
        if (this.state.redirect) {
	        this.props.history.push('/event/list');
        }
    }
    renderEditForm(event) {
        return <form onSubmit={this.handleSubmit.bind(this)}>
            <div>
                <div className="form-group row">
                    <label className=" control-label col-md-12">Title:</label>
                    <div className="col-md-4">
                        <input className="form-control"
                            type="text"
                            value={event.title}
                            onChange={this.handleChange}
                            placeholder="Write a title..." />
                    </div>
                </div>
                <div className="form-group row">
                    <label className=" control-label col-md-12">Description:</label>
                    <div className="col-md-4">
                        <input className="form-control"
                            type="text"
                            value={event.description}
                            onChange={this.handleChange}
                            placeholder="Write a description..." />
                    </div>
                </div>
                <div className="form-group row">
                    <label className=" control-label col-md-12">Type: </label>
                    <div className="col-md-4">
                        <select className="form-control" value={this.state.selectedType} onChange={this.handleChange} >
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
                            selected={event.endDate}
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
            </div>
            <NotificationContainer />
        </form>
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