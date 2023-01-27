import React from "react";
import {ZoomLevelRatioSize} from "../shared/types";

interface ZoomProps{
     zooms: ZoomLevelRatioSize[];
}

interface ZoomState{

}
class SelectZoomComponent extends React.Component<ZoomProps, ZoomState> {


        render () {
            return <div>
                <p>
                    {this.props.zooms[0]?.height}
                </p>
            </div>
            // <div className="zoom-section">
            //     <Box sx={{minWidth: 120}}>
            //         <FormControl fullWidth>
            //             <InputLabel id="demo-simple-select-label">Age</InputLabel>
            //             <Select
            //                 // onChange={this.onChange}
            //             >
            //                 {this.props.zooms.
            //                 {this.props.zooms.map((item) => {
            //                     <MenuItem>
            //                         item.
            //                     </MenuItem>
            //                 })}
            //
            //             </Select>
            //         </FormControl>
            //     </Box>
            // </div>
        };

    onChange = (id: number, value: string) => {
        this.setState((state) => ({
            id: id,
            value: value
        }));
    }
}

export default SelectZoomComponent;