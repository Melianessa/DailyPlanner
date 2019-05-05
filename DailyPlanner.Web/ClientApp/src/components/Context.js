import React from "react";

export const staticData = {
    roleList: [
        {
            name: "Admin",
            value: 0
        },
        {
            name: "Client",
            value: 1
        }
    ],
    sexList: [
        {
            name: "Female",
            value: false
        },
        {
            name: "Male",
            value: true
        }
    ],
    eventTypes: [
        {
            name: "Meeting",
            value: 0
        },
        {
            name: "Reminder",
            value: 1
        },
        {
            name: "Event",
            value: 2
        },
        {
            name: "Task",
            value: 3
        }
    ]
}
export const Context = React.createContext(staticData);