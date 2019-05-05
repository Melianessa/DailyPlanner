import React, { Component } from "react";
import "react-notifications/lib/notifications.css";
import "react-datepicker/dist/react-datepicker.css";
import DatePicker from "react-datepicker";
import { staticData } from "./Context";


export class AddUser extends Component {
    constructor(props) {
        let roleList = staticData.roleList;
        let sexList = staticData.sexList;
        super(props);
        this.state = {
            firstName: "",
            lastName: "",
            dateOfBirth: new Date(),
            phone: "",
            email: "",
            sex: sexList ,
            role: roleList,
            selectedRole: null,
            selectedSex: null,
            redirect: false
        }
        this.handleChangeFName = this.handleChangeFName.bind(this);
        this.handleChangeLName = this.handleChangeLName.bind(this);
        this.handleChangeDate = this.handleChangeDate.bind(this);
        this.handleChangePhone = this.handleChangePhone.bind(this);
        this.handleChangeEmail = this.handleChangeEmail.bind(this);
        this.handleChangeSex = this.handleChangeSex.bind(this);
        this.handleChangeRole = this.handleChangeRole.bind(this);
        this.handleClick = this.handleClick.bind(this);
    }
    handleChangeFName(e) {
        this.setState({ firstName: e.target.value });
    }
    handleChangeLName(e) {
        this.setState({ lastName: e.target.value });
    }
    handleChangeDate(date) {
        this.setState({
            dateOfBirth: date
        });
    }
    handleChangePhone(e) {
        this.setState({ phone: e.target.value });
    }
    handleChangeEmail(e) {
        this.setState({ email: e.target.value });
    }
    handleChangeSex(e) {
        this.setState({ selectedSex: e.target.value });
    }
    handleChangeRole(e) {
        var newRole = e.target.value;
        this.setState({ selectedRole: newRole });
    }
    handleClick() {
        let body = {
            FirstName: this.state.firstName,
            LastName: this.state.lastName,
            DateOfBirth: this.state.dateOfBirth,
            Phone: this.state.phone,
            Email: this.state.email,
            Sex: this.state.selectedSex,
            Role: this.state.selectedRole
        }

        fetch("api/user/create",
            {
                method: "POST",
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(body)
            })//.then(NotificationManager.success("Success message", "User successfully added!", 3000))
            .then(this.setState({ redirect: true }));
    }
    handleCancel() {
	    this.props.history.push("/user/list");
    }
    renderRedirect() {
        if (this.state.redirect) {
            this.props.history.push({
                pathname: "/user/list",
                state: { actionMessage: "added" }
            });
        }
    }
    renderCreateForm() {
        return <div>
            <div className="form-group row">
                <label className=" control-label col-md-12">First Name:</label>
                <div className="col-md-4">
                    <input className="form-control"
                        type="text"
                        value={this.state.firstName}
                        onChange={this.handleChangeFName}
                        placeholder="Write the first name..." />
                </div>
            </div>
            <div className="form-group row">
                <label className=" control-label col-md-12">Last Name:</label>
                <div className="col-md-4">
                    <input className="form-control"
                        type="text"
                        value={this.state.lastName}
                        onChange={this.handleChangeLName}
                        placeholder="Write the last name..." />
                </div>
            </div>
            <div className="form-group row">
                <label className=" control-label col-md-12">Date of birth: </label>
                <div className="col-md-4">
                    <DatePicker className="form-control"
                        selected={this.state.dateOfBirth}
                        onChange={this.handleChangeDate}
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
                        value={this.state.phone}
                        onChange={this.handleChangePhone}
                        placeholder="Write the phone number..." />
                </div>
            </div>
            <div className="form-group row">
                <label className=" control-label col-md-12">Email:</label>
                <div className="col-md-4">
                    <input className="form-control"
                        type="email"
                        value={this.state.email}
                        onChange={this.handleChangeEmail}
                        placeholder="Write the email..." />
                </div>
            </div>
            <div className="form-group row">
                <label className=" control-label col-md-12">Sex: </label>
                <div className="col-md-4">
                    <select className="form-control" value={this.state.selectedSex} onChange={this.handleChangeSex
                    } >
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
                    <select className="form-control" value={this.state.selectedRole} onChange={this
                        .handleChangeRole} >
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
        </div>;
    }
    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCreateForm();

        return (
            <div>
                <h1>Create user</h1>
                <p>Complete the following fields.</p>

                {contents}
            </div>
        );
    }
}