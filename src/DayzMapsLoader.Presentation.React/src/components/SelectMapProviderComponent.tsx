import {MapProvider} from "../shared/types";
import React, {useState} from "react";
import {Box, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent} from "@mui/material";

interface MapProvidersProps {
    providers: MapProvider[];
    customHandleChange: (provider: MapProvider) => void;
}

export default function SelectMapProvider(props: MapProvidersProps) {

    const providers = props.providers;

    function handleChange(event: SelectChangeEvent) {
        props.customHandleChange(providers.find((x) => x.name === event.target.value) ?? providers[0]);
    }

    return (
        <div className="map-provider-container">
            <div className="select-map-provider">
                <FormControl sx={{m: 2, width: 300}}>
                    <InputLabel id="providers-select-label">Provider</InputLabel>
                    <Select labelId="providers-select-label" label="providers" onChange={handleChange}>
                        {
                            providers.map((provider) => (
                                <MenuItem key={provider.name} value={provider.name}>
                                    {
                                        provider.name
                                    }
                                </MenuItem>
                            ))
                        }
                    </Select>
                </FormControl>
            </div>
        </div>
    );
}
