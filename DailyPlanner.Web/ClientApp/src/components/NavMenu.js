import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import './NavMenu.css';

export class NavMenu extends Component {
    displayName = NavMenu.name

    render() {
        return (
            <Navbar inverse fixedTop fluid collapseOnSelect>
                <Navbar.Header>
                    <Navbar.Brand>
                        <Link to={'/'}>Daily Planner</Link>
                    </Navbar.Brand>
                    <Navbar.Toggle />
                </Navbar.Header>
                <Navbar.Collapse>
                    <Nav>
                        <LinkContainer to={'/'} exact>
                            <NavItem>
                                <Glyphicon glyph='home' /> Home
              </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/user/list'}>
                            <NavItem>
                                <Glyphicon glyph='user' /> User
              </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/event/list'}>
                            <NavItem>
                                <Glyphicon glyph='list-alt' /> Events
              </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/event/create'}>
		                    <NavItem>
                                <Glyphicon glyph='plus-sign' /> Create new event
		                    </NavItem>
                        </LinkContainer>
	                    <LinkContainer to={'/user/create'}>
		                    <NavItem>
                                <Glyphicon glyph='edit' /> Create new user
		                    </NavItem>
	                    </LinkContainer>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        );
    }
}
