import {MapProvider, MapType} from "../shared/types";
import {FormControl, InputLabel, MenuItem, Select, SelectChangeEvent} from "@mui/material";
import React from "react";

interface TypeProps{
    types: MapType[];
    customHandleChange: (type: MapType) => void
}

export default function SelectMapType(props: TypeProps){

    const types = props.types;

    function handleChange(event: SelectChangeEvent) {
        props.customHandleChange(types.find((x) => x.name === event.target.value) ?? types[0]);
    }

    return (<div className='map-types'>
        <FormControl sx={{m: 2, width: 300}}>
            <InputLabel id="map-types-select-label">Select map type</InputLabel>
            <Select labelId="map-types-select-label" label="Select map type"
            onChange={handleChange}>
                {
                    types.map((type) => (
                        <MenuItem key={type.name} value={type.id}>
                            {
                                type.name
                            }
                        </MenuItem>
                    ))
                }
            </Select>
        </FormControl>
    </div>)
}