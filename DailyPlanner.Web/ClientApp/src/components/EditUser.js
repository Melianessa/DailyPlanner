import React, { Component } from 'react';
import { RouteComponentProps, Router, Route } from 'react-router';
import { NavLink } from "reactstrap";
import { Link, Redirect } from 'react-router-dom';
import 'react-notifications/lib/notifications.css';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import "react-datepicker/dist/react-datepicker.css";
import DatePicker from "react-datepicker";
import { UserList } from './UserList';
import "./NavMenu.css";
import "./style.css";
import { confirmAlert } from "react-confirm-alert"; // Import
import "react-confirm-alert/src/react-confirm-alert.css";

export class EditUser extends Component {
    static displayName = EditUser.name;
    constructor(props) {
        super(props);
        let roleList = [{ name: "Admin", value: 0 }, { name: "Client", value: 1 }];
        let sexList = [{ name: "Male", value: true }, { name: "Female", value: false }];
        this.state = {
            user: [],
            firstName: '',
            lastName: '',
            dateOfBirth: new Date(),
            phone: '',
            email: '',
            sex: sexList,
            role: roleList,
            selectedRole: null,
            selectedSex: null,
            redirect: false,
            loading: true
        }

        this.handleClick = this.handleClick.bind(this);
        this.startPage = this.startPage.bind(this);
        this.startPage(this.props.match.params.id);
    }
    startPage(id) {
        fetch('api/user/edit/' + id)
            .then(response => {
                const json = response.json();
                console.log(json);
                return json;
            }).then(data => {
                console.log(data);
                console.log(this.state.user);
                this.setState({
                    user: data, loading: false, selectedRole: data.role, selectedSex: data.sex
                });
                console.log(this.state.event);
            });
    }
    handleChange(propertyName, event) {
        const user = this.state.user;
        if (propertyName === "dateOfBirth") {
            user[propertyName] = event;
        } else {
            user[propertyName] = event.target.value;
        }

        this.setState({ user: user });
    }
    handleClick(id) {
        let body = {
            FirstName: this.state.user.firstName,
            LastName: this.state.user.lastName,
            DateOfBirth: this.state.user.dateOfBirth,
            Phone: this.state.user.phone,
            Email: this.state.user.email,
            Sex: this.state.user.selectedSex,
            Role: this.state.user.selectedRole,
            Id: this.state.user.id
        }
        this.setState({ redirect: true });
        fetch('api/user/edit/' + id,
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
		        console.log(this.state.user);
		        this.setState({user: data});
		        console.log(this.state.event);
	        })
            .then((responseJson) => {
                this.props.history.push('/user/list');
            });
    }
    handleSubmit() {
        NotificationManager.success('Success message', 'User successfully edited!', 1000000);
    }
    handleCancel() {
        this.props.history.push('/user/list');
    }
    renderRedirect() {
        if (this.state.redirect) {
            this.props.history.push('/user/list');
        }
    }
    renderEditForm(user) {
        return <form onSubmit={this.handleSubmit.bind(this)}>
            <div>
                <div className="form-group row">
                    <label className=" control-label col-md-12">First Name:</label>
                    <div className="col-md-4">
                        <input className="form-control"
                            type="text"
                            value={user.firstName}
                            onChange={this.handleChange.bind(this, 'firstName')} />
                    </div>
                </div>
                <div className="form-group row">
                    <label className=" control-label col-md-12">Last Name:</label>
                    <div className="col-md-4">
                        <input className="form-control"
                            type="text"
                            value={user.lastName}
                            onChange={this.handleChange.bind(this, 'lastName')} />
                    </div>
                </div>
                <div className="form-group row">
                    <label className=" control-label col-md-12">Date of birth: </label>
                    <div className="col-md-4">
                        <DatePicker className="form-control"
                            selected={user.dateOfBirth}
                            onChange={this.handleChange.bind(this, 'dateOfBirth')}
                            showYearDropdown
                            scrollableYearDropdown
                            yearDropdownItemNumber={15}
                            showMonthDropdown
                        />
                    </div>
                </div>
                <div className="form-group row">
                    <label className=" control-label col-md-12">Phone:</label>
                    <div className="col-md-4">
                        <input className="form-control"
                            type="phone"
                            value={user.phone}
                            onChange={this.handleChange.bind(this, 'phone')} />
                    </div>
                </div>
                <div className="form-group row">
                    <label className=" control-label col-md-12">Email:</label>
                    <div className="col-md-4">
                        <input className="form-control"
                            type="email"
                            value={user.email}
                            onChange={this.handleChange.bind(this, 'email')} />
                    </div>
                </div>
                <div className="form-group row">
                    <label className=" control-label col-md-12">Sex: </label>
                    <div className="col-md-4">
                        <select className="form-control" value={user.sex ? "Male" : "Female"} onChange={this.handleChange.bind(this, 'sex')} >
                            <option value="">-- Select sex --</option>
                            {this.state.sex.map(p =>
                                <option key={p.name} value={p.value}>{p.name}</option>

                            )}
                        </select>
                    </div>
                </div >
                <div className="form-group row">
                    <label className=" control-label col-md-12">Role: </label>
                    <div className="col-md-4">
                        <select className="form-control" value={user.role === 1 ? "Client" : "Admin"} onChange={this.handleChange.bind(this, 'role')} >
                            <option value="">-- Select role --</option>
                            {this.state.role.map(p =>
                                <option key={p.name} value={p.value}>{p.name}</option>

                            )}
                        </select>
                    </div>
                </div >

                <div className="form-group">
                    <button className="btn btn-success" onClick={this.handleClick}>Save user</button>
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
            : this.renderEditForm(this.state.user);
        return (
            <div>
                <h1>Edit user</h1>
                <p>Edit the following fields.</p>

                {contents}
            </div>
        );
    }
}