import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { EventList } from './components/EventList';
import { AddEvent } from "./components/AddEvent";
import { TestComponent } from "./components/TestComponent";

export default class App extends Component {
    displayName = App.name

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/counter' component={Counter} />
                <Route path='/fetchdata' component={FetchData} />
                <Route path='/eventlist' component={EventList} />
                <Route path='/create' component={AddEvent} />
                <Route path='/test' component={TestComponent} />
            </Layout>
        );
    }
}
