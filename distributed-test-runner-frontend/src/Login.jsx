import React, { useState } from 'react'
import { Box, Button, TextField, Typography } from '@mui/material'
import LoginOutlinedIcon from '@mui/icons-material/LoginOutlined';
import AppRegistrationOutlinedIcon from '@mui/icons-material/AppRegistrationOutlined';
import { useForm } from "react-hook-form"
import { Link } from "react-router-dom"

const Login = () => {
    const { register, handleSubmit, formState: { errors } } = useForm();
    return (
        <form onSubmit={handleSubmit((data) => {
            //send a post request to the backend here...
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
                <Typography variant='h4' padding={3}>Login</Typography>
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
                <Button
                    disabled={Boolean(errors.email) || Boolean(errors.password)}
                    endIcon={<LoginOutlinedIcon />}
                    type="submit"
                    sx={{ marginTop: 3, borderRadius: 3, marginBottom: 3 }}
                    variant='contained'
                    color='warning'>Login</Button>
                <Link to="/sign-up">Sign Up</Link>
            </Box>
        </form>
    )
}

export default Login