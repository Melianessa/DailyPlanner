import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { EventList } from "./components/EventComponents/EventList";
import { AddEvent } from "./components/EventComponents/AddEvent";
import { UserList } from "./components/UserComponents/UserList";
import { AddUser } from "./components/UserComponents/AddUser";
import { EditEvent } from "./components/EventComponents/EditEvent";
import { EditUser } from "./components/UserComponents/EditUser";
import { Login } from "./components/AuthComponents/Login";
import { Register } from "./components/AuthComponents/Register";


export default class App extends Component {
    displayName = App.name

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/event/list' component={EventList} />
                <Route path='/event/create' component={AddEvent} />
                <Route path='/user/create' component={AddUser} />
                <Route path='/user/list' component={UserList} />
                <Route path='/event/edit/:id' component={EditEvent} />
                <Route path='/user/edit/:id' component={EditUser} />
                <Route path='/login' component={Login} />
                <Route path='/register' component={Register} />
                <Route path='/swagger' component={() => { window.location = '/swagger'; return null; }} />
            </Layout>
        );
    }
}
