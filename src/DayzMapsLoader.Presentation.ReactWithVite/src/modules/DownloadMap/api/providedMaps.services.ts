import axios from "axios";
import {ProvidedMap} from "../../../helpers/types";

axios.defaults.baseURL = 'https://localhost:7188/api';
export const ProvidedMapsService = {
    async getAll(){
        return await axios.get<ProvidedMap[]>('/provided-maps/get-all');
    }
}