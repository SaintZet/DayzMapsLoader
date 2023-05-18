import {defaultValue, Map, MapType, ProvidedMap, SelectItem, Zoom, ZoomLevelRatioSize} from "../../helpers/types";
import {Selector} from "../../ui/DownloadMap/LoaderSelector";
import {getSelectItems} from "../../modules/DownloadMap/helpers/switchLoaderDataType";
import React, {ReactNode, useEffect, useMemo} from "react";
import {useContextStore} from "../../modules/DownloadMap/context/providedMapsContext";
import {useFormik} from "formik";
import {SelectChangeEvent} from "@mui/material";
import * as yup from "yup";

interface Form {
    maxZoom: number;
}

const formSchema = yup.object<Form>({
    zoom: yup.number().required().min(0, 'Choose zoom').max(8).default(defaultValue),
}).required()

export default function SelectZoom() {
    const {providedMaps, selectedProvider, selectedMap, selectedType, setSelectedZoom} = useContextStore();
    const minZoom = 0;
    const maxZoom = providedMaps.find(x => x.map.id === selectedMap && x.mapProvider.id === selectedProvider && x.mapType.id === selectedType)?.maxMapLevel ?? 0;
    const zoomRange: Array<Zoom> = new Array(maxZoom - minZoom + 1).fill(undefined).map((_, i) => i + minZoom)
        .map(function (element) {
            return {
                zoom: element
            }
        });

    const options: Set<SelectItem<number>> = getSelectItems<Zoom[]>(zoomRange);

    const formikDefaultProps = useFormik({
        initialValues: formSchema.cast({value: -1}) as Form,
        enableReinitialize: true,
        validationSchema: formSchema,
        onSubmit: (form) => {
        }
    });
    const onHandleChange = (event: SelectChangeEvent<number>) => {
        const value = event.target.value as number;
        setSelectedZoom(value);
        formikDefaultProps.setFieldValue("maxZoom", value);
    }
    useEffect(() => {
        if (selectedMap === defaultValue) {
            formikDefaultProps.resetForm();
        }
    }, [selectedMap])

    return (
        <Selector<Form>
            disabled={selectedType === defaultValue || !selectedType}
            label="maxZoom"
            onHandleChange={onHandleChange}
            {...formikDefaultProps}
            controlName='maxZoom'
            options={options}
            showError
        />
    );

}
