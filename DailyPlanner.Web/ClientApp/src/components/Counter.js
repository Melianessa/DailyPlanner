import React, { Component } from 'react';
import scheduler from 'dhtmlx-scheduler';


export class Counter extends Component {
    componentDidMount() {
        scheduler.init('scheduler_here', new Date(2019, 0, 20), "week");
    } 
    render() {
        return <div id="scheduler_here" className="dhx_cal_container" style=
            {{ width: '100%', height: '100%' }}>
            
            <div className="dhx_cal_navline">
                <div className="dhx_cal_prev_button">&nbsp;</div>
                <div className="dhx_cal_next_button">&nbsp;</div>
                <div className="dhx_cal_today_button"></div>
                <div className="dhx_cal_date"></div>
                <div className="dhx_cal_tab" name="day_tab"></div>
                <div className="dhx_cal_tab" name="week_tab"></div>
                <div className="dhx_cal_tab" name="month_tab"></div>
            </div>
            <div className="dhx_cal_header"></div>
            <div className="dhx_cal_data"></div>
            
        </div>
    }
}