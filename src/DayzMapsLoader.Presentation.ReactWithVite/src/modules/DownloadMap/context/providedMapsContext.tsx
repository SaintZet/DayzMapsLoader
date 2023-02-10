import {ProvidedMap} from "../../../helpers/types";
import {ProvidedMapsService} from "../api/providedMaps.services";
import React from "react";

const initialProvidedMaps: ProvidedMap[] = [];
export const providedMaps = await ProvidedMapsService.getAll().then(data => data.data);

export const ProvidedMapsContext = React.createContext<ProvidedMap[]>(initialProvidedMaps);