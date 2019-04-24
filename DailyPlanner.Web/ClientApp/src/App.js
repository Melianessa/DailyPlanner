import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { EventList } from './components/EventList';
import { AddEvent } from "./components/AddEvent";
import { UserList } from "./components/UserList";
import { AddUser } from "./components/AddUser";


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
                </Layout>
        );
    }
}
