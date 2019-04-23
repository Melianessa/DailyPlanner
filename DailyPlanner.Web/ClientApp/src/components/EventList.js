import React, { Component } from "react";
import { NavLink } from "reactstrap";
import { Link } from "react-router-dom";
import "./NavMenu.css";
import "./style.css";
import { confirmAlert } from "react-confirm-alert"; // Import
import "react-confirm-alert/src/react-confirm-alert.css";
import "react-datepicker/dist/react-datepicker.css";
import DatePicker from "react-datepicker";

export class EventList extends Component {
    static displayName = EventList.name;

    constructor(props) {
        super(props);
        let date = new Date();
        this.state = { events: [], loading: true, selectedDay: date };
        this.handleDelete = this.handleDelete.bind(this);
        this.helperDelete = this.helperDelete.bind(this);
        this.handleDayClick = this.handleDayClick.bind(this);
        this.handleGetAll = this.handleGetAll.bind(this);
        this.handleGetAll(new Date());
    }
    handleGetAll(day) {
        let reqBody = { date: day.toLocaleDateString("en-US") };
        fetch('api/event/getByDate', {
            method: "POST",
            headers: {
                //"Accept": "application/json",
                //application/x-www-form-urlencoded for methods without [FromBody]
                //application/json for methods with [FromBody]
                "Content-Type": "application/x-www-form-urlencoded"
            },
            body: new URLSearchParams(reqBody).toString()
        }).then(response => {
            const json = response.json();
            console.log(json);
            return json;
        }).then(data => {
            console.log(data);
            this.handleDayClick(day);
            this.setState({
                events: data, loading: false
            });
            console.log(this.state.events);
        });
    }

    handleDayClick(day) {
        this.setState({ selectedDay: day });
        console.log(this.state.selectedDay);
    }
    helperDelete(id) {
        fetch('api/event/delete/' + id,
            {
                method: "DELETE"
            })
            .then(this.setState({
                events: this.state.events.filter((rec) => {
                    return (rec.id !== id);
                })
            })

            );

    }

    handleDelete(id) {
        confirmAlert({
            title: "Confirm to submit",
            message: "Do you want to delete these event?",
            buttons: [
                {
                    label: "Yes",
                    onClick: () => this.helperDelete(id)
                },
                {
                    label: "No",
                    onClick: () => { return; }
                }
            ]
        });
    }


    handleEdit(id) {
        this.props.history.push('api/event/update/' + { id });
    }
    renderEvent(events) {
        return (
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>Date: <DatePicker onChange={this.handleGetAll} selected={this.state.selectedDay} />
                        </th>
                        <th>Event</th>
                    </tr>
                </thead>
                <tbody>
                    {events.map(ev =>
                        <tr key={ev.id}>
                            <td className="event-date-header">
                                <div>{new Date(ev.startDate).toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" })}</div>
                                <div>{new Date(ev.endDate).toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" })}</div>
                            </td>
                            <td>
                                <div>{ev.title}</div>
                                <div className="event-date-header">{ev.description}</div>
                            </td>
                            <td>
                                <button className="btn btn-warning" onClick={() => this.handleEdit(ev.id)}>Edit</button>
                                <button className="btn btn-danger" onClick={() => this.handleDelete(ev.id)}>Delete</button>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderEvent(this.state.events);

        return (
            <div>
                <h1>Events list</h1>
                <p>This component demonstrates events list.</p>
                <div className="button-group">
                    <NavLink tag={Link} className="btn btn-success" to="/event/create">Create new</NavLink>
                </div>
                {contents}
            </div>
        );
    }
}