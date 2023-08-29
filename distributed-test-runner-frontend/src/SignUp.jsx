import React, { useRef, useState } from 'react'
import { Alert, Box, Button, Container, Snackbar, TextField, Typography, useMediaQuery, useTheme } from '@mui/material'
import AppRegistrationOutlinedIcon from '@mui/icons-material/AppRegistrationOutlined';
import { Link } from "react-router-dom"
import { useForm } from "react-hook-form"
import axios from 'axios';
import Logo from './Logo';
import LoginIcon from '@mui/icons-material/Login';


const API_ENDPOINT = "/auth/register-user";

const SignUp = () => {
    const [showSnackbar, setShowSnackbar] = useState(false);
    const [snackbarSeverity, setSnackbarSeverity] = useState("success");
    const [alertMessage, setAlertMessage] = useState();
    const handleClose = (event, reason) => {
        setShowSnackbar(false);
    };

    const { register, handleSubmit, watch, formState: { errors } } = useForm();
    const password = useRef({});
    password.current = watch("password", "");
    const theme = useTheme();
    const isSmallScreen = useMediaQuery(theme.breakpoints.down('sm'));
    const isLargeScreen = useMediaQuery(theme.breakpoints.up('md'));

    return (
        <Container>
            <form onSubmit={handleSubmit(async (data) => {
                //send a post request to the backend here...
                await axios.post('/auth/sign-up', data).then(response =>
                    console.log(response)
                )
                    .catch(function (error) {
                        setSnackbarSeverity("error");
                        setAlertMessage(error.response ? error.response.data : "Beklenmedik bir hata oluştu!");
                        setShowSnackbar(true);
                    });
            })}>

                <Box
                    maxWidth={isSmallScreen ? "100%" : isLargeScreen ? "40%" : "75%"}
                    display="flex"
                    flexDirection="column"
                    alignItems="center"
                    justifyContent="center"
                    margin="auto"
                    marginTop={2}
                    borderRadius={5}
                    boxShadow={'5px 5px 10px #ccc'}
                >

                    <Logo />
                    <Typography variant='h4' padding={1}>Sign Up</Typography>
                    <TextField
                        error={Boolean(errors?.email)}
                        helperText={Boolean(errors?.email) ? <Typography>Invalid Email</Typography> : null}
                        name="email"
                        variant='outlined'
                        label="Email"
                        InputProps={{ sx: { borderRadius: 3 } }}
                        sx={{ width: "75%", mt: 1, mb: 1 }}
                        {...register("email", { validate: (val) => /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(val) })}
                    />
                    <TextField
                        error={Boolean(errors?.password)}
                        helperText={Boolean(errors?.password) ? "Invalid password" : null}
                        InputProps={{ sx: { borderRadius: 3 } }}
                        margin="normal"
                        variant='outlined'
                        sx={{ width: "75%", mt: 1, mb: 1 }}
                        type="password"
                        {...register("password", { minLength: 6, required: true })}
                        label="Password"
                    />
                    <TextField
                        error={Boolean(errors?.repeatedPassword)}
                        InputProps={{ sx: { borderRadius: 3 } }}
                        helperText={Boolean(errors?.repeatedPassword) ? "Passwords do not match" : null}
                        margin="normal"
                        variant='outlined'
                        sx={{ width: "75%", mt: 1, mb: 1 }}
                        type="password"
                        {...register("repeatedPassword", { minLength: 6, required: true, validate: (rep) => rep === password.current || "The passwords do not match" })}
                        label="Repeat Password"
                    />

                    <Button
                        disabled={Boolean(errors.email) || Boolean(errors.password)}
                        endIcon={<AppRegistrationOutlinedIcon />}
                        type="submit"
                        size='lg'
                        sx={{ borderRadius: 3, marginBottom: 1, width: "60%", textTransform: "none" }}
                        variant='contained'

                        color='warning'>Sign Up</Button>
                    <Button
                        sx={{ borderRadius: 3, mb: 1, width: "50%", textTransform: "none" }}
                        variant="outlined"
                        endIcon={<LoginIcon />}
                        component={Link} to="/login">
                        Registered?
                    </Button>

                </Box>

            </form>
            <Snackbar
                open={showSnackbar}
                anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                }}>
                <Alert severity={snackbarSeverity} onClose={handleClose}>
                    An error is occurred
                </Alert>
            </Snackbar>
        </Container>

    )
}

export default SignUp