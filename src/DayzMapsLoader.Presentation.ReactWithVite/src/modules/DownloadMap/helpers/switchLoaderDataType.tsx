import {
    InterfaceMatcher,
    LoaderTypes,
    Map,
    MapProvider,
    MapType, SelectItem, Zoom,
    ZoomLevelRatioSize
} from "../../../helpers/types";
import React, {ReactNode} from "react";
import {interfaceMatcher} from "./instanceOfMatcher";
import {MenuItem} from "@mui/material";

export function getSelectItems<T extends LoaderTypes>(data: T): Set<SelectItem<number>> {
    let items:Set<SelectItem<number>> = new Set<SelectItem<number>>();
    const matchValue = interfaceMatcher(data);
    switch (matchValue) {
        case InterfaceMatcher.ProviderArray: {
            (data as MapProvider[]).forEach(provider =>
                items.add({text: provider.name, value: provider.id})
            );
            break;
        }
        case InterfaceMatcher.MapTypeArray: {
            (data as MapType[]).forEach(type =>
                items.add({text: type.name, value: type.id})
            );
            break;
        }
        case InterfaceMatcher.MapArray: {
            (data as Map[]).forEach(map =>
                items.add({text: map.name, value: map.id}));
            break;
        }
        case InterfaceMatcher.ZoomArray: {
            (data as Zoom[]).forEach(zoom => {
                items.add({text: zoom.zoom.toString(), value: zoom.zoom});
            });
            break;
        }
    }
    return items;
}