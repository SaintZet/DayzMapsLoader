import React, {useState} from "react";
import {MapProvider, Map} from "../shared/types";
import {FormControl, InputLabel, MenuItem, Select} from "@mui/material";
import {MapProviderNameDecorator} from "../utils";

interface MapsProps {
    provider: MapProvider;
    selectedMap?: Map | null;
}

export default function SelectMap(props: MapsProps) {

    const [provider] = useState<MapProvider>(props.provider);
    const [maps] = useState<Array<Map>>(props.provider?.maps ?? []);

    return (
        <div className="select-map">
            <FormControl sx={{m: 2, width: 300}}>
                <InputLabel id="maps-select-label">Map</InputLabel>
                <Select labelId="maps-select-label" label="map"
                        defaultValue={maps[0]?.name}>
                    {
                        maps?.map((map) => (
                            <MenuItem key={map.name} value={map.name}>
                                {
                                    map?.nameForProvider
                                }
                            </MenuItem>
                        ))
                    }
                </Select>
            </FormControl>
        </div>
    )
}