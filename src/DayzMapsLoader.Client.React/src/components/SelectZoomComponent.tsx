import {isNumberObject} from "util/types";
import {Simulate} from "react-dom/test-utils";
import dragOver = Simulate.dragOver;
import {FormControl, InputLabel, MenuItem, Select, SelectChangeEvent} from "@mui/material";
import React from "react";
import {MapType} from "../shared/types";

interface ZoomProps{
    maxZoom: number;
    customHandleChange: (zoom: number) => void

}

export default function SelectZoom(props: ZoomProps){
    const minZoom: number = 0;
    const maxZoom: number = props.maxZoom;
    const zoomRange = new Array(maxZoom - minZoom + 1).fill(undefined).map((_,i) => i + minZoom);

    function handleChange(event: SelectChangeEvent) {
        props.customHandleChange(zoomRange[parseInt(event.target.value)]);
    }

    return(<div className='select-zoom-section'>
        <FormControl sx={{m: 2, width: 300}}>
            <InputLabel id="map-zoom-select-label">Select zoom</InputLabel>
            <Select labelId="map-zoom-select-label" label="Select zoom" onChange={handleChange}>
                {
                    zoomRange.map((zoom) => (
                        <MenuItem key={zoom} value={zoom}>
                            {
                                zoom
                            }
                        </MenuItem>
                    ))
                }
            </Select>
        </FormControl>
    </div>)
}