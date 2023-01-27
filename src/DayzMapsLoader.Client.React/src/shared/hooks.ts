import axios from "axios";
import {useEffect, useState} from "react";
import {devPathToServer} from "./consts";

axios.defaults.baseURL = 'https://localhost:7208/api';
interface axiosResponse<T>{
    providers: T[];
    error: string;
    loading: boolean;
}
export default function useAxios<T>(endpoint: string): axiosResponse<T> {
    const [data, setData] = useState(new Array<T>);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(true);

    const fetchData = () => {

        axios.
            get(`${devPathToServer}${endpoint}`)
            .then((res) => {
                setData(res.data);
            })
            .catch((error) => {
                setError(error);
            })
            .finally(() => {
                setLoading(false);
            });
    }
    useEffect(() => {
        fetchData()
    }, []);

    return {providers: data, error, loading};
}
