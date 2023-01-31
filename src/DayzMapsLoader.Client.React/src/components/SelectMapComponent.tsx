import React from "react";
import {Map} from "../shared/types";
import {FormControl, InputLabel, MenuItem, Select} from "@mui/material";

interface MapsProps {
    maps: Map[];
}

export default function SelectMap(props: MapsProps) {
    const maps = props.maps;

    return (
        <div className="select-map">
            <FormControl sx={{m: 2, width: 300}}>
                <InputLabel id="maps-select-label">Select map</InputLabel>
                <Select labelId="maps-select-label" label="Select map">
                    {
                        maps?.map((map) => (
                            <MenuItem key={map.name} value={map.name}>
                                {
                                    map?.name
                                }
                            </MenuItem>
                        ))
                    }
                </Select>
            </FormControl>
        </div>
    )
}