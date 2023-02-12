import {
    InterfaceMatcher,
    LoaderTypes,
    Map,
    MapProvider,
    MapType, SelectItem,
    ZoomLevelRatioSize
} from "../../../helpers/types";
import React, {ReactNode} from "react";
import {interfaceMatcher} from "../../../helpers/instanceOfMatcher";
import {MenuItem} from "@mui/material";

export function getSelectItems<T extends LoaderTypes>(data: T): SelectItem<number>[] {
    let items:SelectItem<number>[] =[];
    switch (interfaceMatcher(data)) {
        case InterfaceMatcher.ProviderArray: {
            (data as MapProvider[]).forEach(provider =>
                items.push({text: provider.name, value: provider.id})
            );
            break;
        }
        case InterfaceMatcher.MapTypeArray: {
            (data as MapType[]).forEach(type =>
                items.push({text: type.name, value: type.id})
            );
            break;
        }
        case InterfaceMatcher.MapArray: {
            (data as Map[]).forEach(map =>
                items.push({text: map.name, value: map.id}));
            break;
        }
        case InterfaceMatcher.ZoomArray: {
            (data as number[]).forEach(zoom => {
                items.push({text: zoom.toString(), value: zoom});
            });
            break;
        }
    }
    return items;
}