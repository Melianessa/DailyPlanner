import React, { Component } from 'react';

export class Home extends Component {
    displayName = Home.name

    render() {
        return (
            <div>
                <h1>Hello, user!</h1>
                <p>Welcome to your daily planner, where you can:</p>
                <ul>
                    <li><strong>Plan your business day</strong></li>
                    <li><strong>Plan your weekends</strong></li>
                    <li><strong>Create and Manage Events very easy</strong></li>
                </ul>
                <p>The <code>ClientApp</code> subdirectory is a standard React application based on the <code>create-react-app</code> template. If you open a command prompt in that directory, you can run <code>npm</code> commands such as <code>npm test</code> or <code>npm install</code>.</p>
            </div>
        );
    }
}
