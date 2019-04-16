import React, { Component } from 'react';
import { NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import './style.css';
import { confirmAlert } from 'react-confirm-alert'; // Import
import 'react-confirm-alert/src/react-confirm-alert.css';
import DayPicker from 'react-day-picker';
import 'react-day-picker/lib/style.css';

export class EventList extends Component {
    static displayName = EventList.name;

    constructor(props) {
        super(props);
        this.state = { events: [], loading: true, selectedDay: undefined };
        this.handleDelete = this.handleDelete.bind(this);
        this.helperDelete = this.helperDelete.bind(this);
        this.handleDayClick = this.handleDayClick.bind(this);
        this.handleGetAll = this.handleGetAll.bind(this);
        //this.onClick = this.onClick.bind(this);
        this.handleGetAll();
    }
    handleGetAll(day) {
        fetch('api/event/postByDate', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(day)
        })
            .then(response => response.json())
            .then(data => {
                console.log(data);
                this.handleDayClick(day);
                //const newData = this.state.events.filter(item => item.startDate.toLocaleDateString() === this.state.selectedDay.toLocaleDateString());
                this.setState({
                    events: data, loading: false
                });
            });
    }
    
    handleDayClick(day) {
        this.setState({ selectedDay: day });
        console.log(this.state.selectedDay);
    }
    helperDelete(id) {
        fetch('api/event/' + id,
            {
                method: 'DELETE'
            })
            .then(data =>
                this.setState({
                    events: this.state.events.filter((rec) => {
                        return (rec.id !== id);
                    })
                })

            );
    }

    handleDelete(id) {
        confirmAlert({
            title: 'Confirm to submit',
            message: 'Do you want to delete these event?',
            buttons: [
                {
                    label: 'Yes',
                    onClick: () => this.helperDelete(id)
                },
                {
                    label: 'No',
                    onClick: () => { return; }
                }
            ]
        });
    }


    handleEdit(id) {
        this.props.history.push(`api/event/${id}`);
    }
    renderEvent(events) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Date: {this.state.selectedDay
                            ? this.state.selectedDay.toLocaleDateString()
                            : 'Please select a day'}
                            <div>
                                <DayPicker onDayClick={this.handleGetAll} selectedDays={this.state.selectedDay} />
                            </div>
                        </th>
                        <th>Event</th>
                    </tr>
                </thead>
                <tbody>
                    {events.map(ev =>
                        <tr key={ev.id}>
                            <td className="event-date-header">
                                <div>{new Date(ev.startDate).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}</div>
                                <div>{new Date(ev.endDate).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}</div>
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