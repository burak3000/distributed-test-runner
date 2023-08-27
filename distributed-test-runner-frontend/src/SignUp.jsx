import React, { useRef, useState } from 'react'
import { Box, Button, TextField, Typography } from '@mui/material'
import AppRegistrationOutlinedIcon from '@mui/icons-material/AppRegistrationOutlined';
import { Link } from "react-router-dom"
import { useForm } from "react-hook-form"
import axiosInstance from './api/axios';
import axios from 'axios';

const API_ENDPOINT = "/auth/register-user";

const SignUp = () => {
    const { register, handleSubmit, watch, formState: { errors } } = useForm();
    const password = useRef({});
    password.current = watch("password", "");

    return (
        <form onSubmit={handleSubmit(async (data) => {
            //send a post request to the backend here...
            await axios.post('http://localhost:5050/auth/sign-up', data).then(response=>console.log(response));
        })}>
            <Box
                maxWidth={"50%"}
                display="flex"
                flexDirection="column"
                alignItems="center"
                justifyContent="center"
                margin="auto"
                marginTop={5}
                padding={5}
                borderRadius={5}
                boxShadow={'5px 5px 10px #ccc'}
            >
                <Typography variant='h4' padding={3}>Sign Up</Typography>
                <TextField
                    error={Boolean(errors?.email)}
                    helperText={Boolean(errors?.email) ? <Typography>Invalid Email</Typography> : null}
                    name="email"
                    margin="normal"
                    variant='outlined'
                    label="Email"
                    {...register("email", { validate: (val) => /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(val) })}
                />
                <TextField
                    error={Boolean(errors?.password)}
                    helperText={Boolean(errors?.password) ? "Invalid password" : null}
                    margin="normal"
                    variant='outlined'
                    type="password"
                    {...register("password", { minLength: 6, required: true })}
                    label="Password"
                />
                <TextField
                    error={Boolean(errors?.repeatedPassword)}
                    helperText={Boolean(errors?.repeatedPassword) ? "Passwords do not match" : null}
                    margin="normal"
                    variant='outlined'
                    type="password"
                    {...register("repeatedPassword", { minLength: 6, required: true, validate: (rep) => rep === password.current || "The passwords do not match" })}
                    label="Repeat Password"
                />

                <Button
                    disabled={Boolean(errors.email) || Boolean(errors.password)}
                    endIcon={<AppRegistrationOutlinedIcon />}
                    type="submit"
                    sx={{ marginTop: 3, borderRadius: 3, marginBottom: 3 }}
                    variant='contained'
                    color='warning'>Sign Up</Button>
                <Link style={{ margin: 3 }} to="/login">Login</Link>
            </Box>
        </form>
    )
}

export default SignUp