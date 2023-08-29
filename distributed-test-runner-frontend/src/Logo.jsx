import { Box, Typography } from '@mui/material'
import React from 'react'
import BugReportIcon from '@mui/icons-material/BugReport';
import ScienceIcon from '@mui/icons-material/Science';
import CloudUploadIcon from '@mui/icons-material/CloudUpload';
import VerifiedIcon from '@mui/icons-material/Verified';
const Logo = () => {
    return (
        <Box sx={{ display: "flex" }}>
            <CloudUploadIcon sx={{ color: "orange" }} />
            <ScienceIcon sx={{ color: "#5DADE2" }} />
            <BugReportIcon sx={{ color: "red" }} />
            <VerifiedIcon sx={{ color: "green" }} />
        </Box>
    )
}
export default Logo