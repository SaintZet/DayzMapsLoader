import React from "react";
import {Map, MapProvider} from "../shared/types";
import {FormControl, InputLabel, MenuItem, Select, SelectChangeEvent} from "@mui/material";

interface MapsProps {
    maps: Map[];
    customHandleChange: (map: Map) => void;
}

export default function SelectMap(props: MapsProps) {
    const maps = props.maps;

    function handleChange(event: SelectChangeEvent) {
        props.customHandleChange(maps.find((x) => x.name === event.target.value) ?? maps[0]);
    }

    return (
        <div className="select-map">
            <FormControl sx={{m: 2, width: 300}}>
                <InputLabel id="maps-select-label">Select map</InputLabel>
                <Select labelId="maps-select-label" label="Select map" onChange={handleChange}>
                    {
                        maps.map((map) => (
                            <MenuItem key={map.name} value={map.name}>
                                {
                                    map.name
                                }
                            </MenuItem>
                        ))
                    }
                </Select>
            </FormControl>
        </div>
    )
}