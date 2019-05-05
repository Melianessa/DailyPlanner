import React, { Component } from "react";
import { RouteComponentProps } from "react-router";
import { Link, NavLink } from "react-router-dom";

export class test extends Component {
    constructor(props) {
        super(props);
        let date = new Date();
        this.state = { events: [], loading: true, selectedDay: date, title: "", typeList: [{ name: "Meeting", value: 0 }, { name: "Reminder", value: 1 }, { name: "Event", value: 2 }, { name:"Task", value:3 }] };
        var eventid = this.props.match.params["eventid"];
        if (eventid > 0) {
            fetch("api/event/update/" + eventid)
                .then(response => {
                    const json = response.json();
                    console.log(json);
                    return json;
                })
                .then(data => {
                    console.log(data);
                    this.setState({
                        events: data,
                        loading: false, title: "Edit "
                    });
                });
        } else {
            this.state = { loading: false, events: new Event, title: "Create ", typeList: [{ name: "Meeting", value: 0 }, { name: "Reminder", value: 1 }, { name: "Event", value: 2 }, { name: "Task", value: 3 }] }
        }
        this.handleSave = this.handleSave.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
    }
    handleSave(event, id) {
        event.preventDefault();
        const data = new FormData(event.target);
        if (this.state.events.id) {
            fetch("api/event/update/" + id,
                {
                    method: "PUT",
                    body: data
                }).then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/eventlist");
                });
        } else {
            fetch("api/event/create/" + id,
                {
                    method: "POST",
                    body: data
                }).then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/eventlist");
                });
        }
    }

    handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/eventlist");
    }

    renderCreateEvent(events) {
        return (<form onSubmit={this.handleSave} >
            <div className="form-group row" >
                <input type="hidden" name="eventid" value={this.state.events.id} />
            </div>
            <div className="form-group row">
                <label className=" control-label col-md-12" htmlFor="Title">Title</label>
                <div className="col-md-4">
                    <input className="form-control" type="text" name="title" defaultValue={this.state.events.title
                    } required />
                </div>
            </div>
            <label className=" control-label col-md-12" htmlFor="Description">Description</label>
            <div className="col-md-4">
                <input className="form-control" type="text" name="description" defaultValue={this.state.events.description
                } required />
            </div>
            <div className="form-group row">
                <label className="control-label col-md-12" htmlFor="Type">Type</label>
                <div className="col-md-4">
                    <select className="form-control" data-val="true" name="Type" defaultValue={this.state.events.type
                    } required>
                        <option value="">-- Select type --</option>
                        {this.state.typeList.map(et =>
                            <option value={et.value}>{et.name}</option>
                        )}
                    </select>
                </div>
            </div >
            <div className="form-group">
                <button type="submit" className="btn btn-default">Save</button>
                <button className="btn" onClick={this.handleCancel}>Cancel</button>
            </div >
        </form >
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCreateEvent();

        return <div>
            <h1>{this.state.title}</h1>
            <h3>Employee</h3>
            <hr />
            {contents}
        </div>;
    }
}