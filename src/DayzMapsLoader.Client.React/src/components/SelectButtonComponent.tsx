import {Button, ButtonGroup, MenuItem, Select} from "@mui/material";
import axios from "axios";

interface ButtonProps{
    providerId: number;
    mapId: number;
    typeId: number;
    zoom: number;
}
export default function SelectButton(props: ButtonProps) {
    const [providerId, mapId, typeId, zoom] = [props.providerId, props.mapId, props.typeId, props.zoom];
    const clickFullParamArchiv = () => {
        axios.get(`${axios.defaults.baseURL}/providers/${providerId}/maps/${mapId}/types/${typeId}/zoom/${zoom}/image-archive`);
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