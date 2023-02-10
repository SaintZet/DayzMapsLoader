import {
    InterfaceMatcher,
    LoaderSelectorTypes,
    Map,
    MapProvider,
    MapType,
    ZoomLevelRatioSize
} from "../../../helpers/types";
import React, {ReactNode} from "react";
import {interfaceMatcher} from "../../../helpers/instanceOfMatcher";
import {MenuItem} from "@mui/material";

export function switchType<T extends LoaderSelectorTypes>(data: T): ReactNode {
    let a: Array<ReactNode> = [];
    switch (interfaceMatcher(data)) {
        case InterfaceMatcher.ProviderArray: {
            (data as MapProvider[]).map(provider => {
                a.push(<MenuItem key={provider.name} value={provider.name}>{provider.name}</MenuItem>)
            });
            return a;
        }
        case InterfaceMatcher.MapTypeArray: {
            (data as MapType[]).map(type => {
                a.push(<MenuItem key={type.id} value={type.id}>{type.name}</MenuItem>)
            });
            return;
        }
        case InterfaceMatcher.MapArray: {
            (data as Map[]).map(map => {
                a.push(<MenuItem key={map.id} value={map.id}>{map.name}</MenuItem>)
            });
            return;
        }
        case InterfaceMatcher.ZoomArray: {
            (data as number[]).map(zoom => {
                a.push(<MenuItem key={zoom} value={zoom}>{zoom}</MenuItem>)
            });
            return;
        }
    }
    return <MenuItem key="no data" value="no data">no data</MenuItem>
}