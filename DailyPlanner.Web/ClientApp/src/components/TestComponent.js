import React, { Component } from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';
import 'react-notifications/lib/notifications.css';
import { NotificationContainer, NotificationManager } from 'react-notifications';

export class TestComponent extends Component {
    constructor(props) {
        let typeList = [
            { name: "Meeting", value: 0 }, { name: "Reminder", value: 1 }, { name: "Event", value: 2 },
            { name: "Task", value: 3 }
        ];
        super(props);
        this.state = { title: '', description: '', type: typeList, selectedType: null }
        this.handleChange = this.handleChange.bind(this);
        this.handleChangeType = this.handleChangeType.bind(this);
    }
    handleChange(e) {
        this.setState({ title: e.target.value });
    }
    handleChangeDesc(e) {
        this.setState({ description: e.target.value });
    }
    handleChangeType(e) {
        var newType = e.target.value ;
	    this.setState({ selectedType: newType});
        console.log(e.target.value);
    }
    handleClick(title, description, selectedType) {
        let body = { Title: title, Description: description, Type: selectedType}
        fetch('api/event/create', {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify(body)
        }).then(response => {
            const json = response.json();
            console.log(json);
            return json;
        }).then(data => {
            this.setState({
                comm: data
            });
            console.log(this.state.comm);
        });
    }
    handleSubmit() {
        NotificationManager.success('Success message', 'Event successfully added!', 1000000);
    }
    render() {
        return <form onSubmit={this.handleSubmit.bind(this, this.state.title)}>
            <div>
                <div>
                    <label>
                        Title:
                        <input
                            type="text"
                            value={this.state.title}
                            onChange={this.handleChange}
                            placeholder="Write a title..." />
                    </label>
                </div>
                <div>
                    <label>
                        Description:
	                    <input
                            type="text"
                            value={this.state.description}
                            onChange={this.handleChangeDesc.bind(this)}
                            placeholder="Write a description..." />
                    </label>
                </div>
	            <div>
		            <label>Type</label>
		            <div>
                        <select value={this.state.selectedType} onChange={this.handleChangeType} >
				            <option value="">-- Select type --</option>
                            {this.state.type.map(et =>
                                <option key={et.name} value={et.value}>{et.name}</option>

                            )}
			            </select>
		            </div>
	            </div >
                <button onClick={this.handleClick.bind(this, this.state.title, this.state.description, this.state.selectedType)}>Click</button>
            </div>
            <NotificationContainer />
        </form>

    }
}