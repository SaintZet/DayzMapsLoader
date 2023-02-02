import {Button, ButtonGroup} from "@mui/material";
import axios from "axios";
import { saveAs } from 'file-saver';
interface ButtonProps {
    providerId: number;
    mapId: number;
    typeId: number;
    zoom: number;
}

export default function SelectButton(props: ButtonProps) {
    const [providerId, mapId, typeId, zoom] = [props.providerId, props.mapId, props.typeId, props.zoom];


    const clickFullParamArchiv = () => {
        axios(`${axios.defaults.baseURL}/download-map/providers/${providerId}/maps/${mapId}/types/${typeId}/zoom/${zoom}/image-archive`)
            .then(res => res.data.blob())
            .then(blob => saveAs(blob, 'Map.zip')) // saveAs is a function from the file-saver package.
            .catch((err) => {
                console.log(err.message);
            });
        ;
    }
    const clickFullParamParts = () => {
        alert(`${axios.defaults.baseURL}/providers/${providerId}/maps/${mapId}/types/${typeId}/zoom/${zoom}/image-parts-archive`);
    }
    const clickLessParamArchiv = () => {
        alert(`${axios.defaults.baseURL}/providers/${providerId}/zoom/${zoom}/image-archive`);
    }

    return (
        <div className='select-button-action'>
            <ButtonGroup orientation="vertical">
                <Button onClick={clickFullParamArchiv}>Download with all parameters archive</Button>
                <Button onClick={clickFullParamParts}>Download with all parameters archive</Button>
                <Button onClick={clickLessParamArchiv}>Download with all parameters archive</Button>
            </ButtonGroup>
        </div>
    )
}