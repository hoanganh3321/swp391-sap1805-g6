// @ts-nocheck
import React from 'react';
import { Link } from 'react-router-dom'
import BannerImg from "../../assets/img/banner/jewelry.jpg";
import axios from 'axios';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { loginUser } from '../../redux/apiRequest';
import { useDispatch } from 'react-redux';

const Login = () => {

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const dispatch = useDispatch();
    const navigate = useNavigate();


    const handleLogin = async (e) => {
        e.preventDefault();
        const newUser = {
            email: email,
            password: password,

        }
        loginUser(newUser, dispatch, navigate);

    };


    return (
        <form onSubmit={handleLogin}>
            <div className="min-h-[550px] flex justify-center items-center py-12 sm:py-0">
                <div className="container">
                    <div className="grid items-center grid-cols-1 gap-6 sm:grid-cols-2">
                        {/* text details section */}
                        <div className="flex flex-col justify-center gap-6 sm:pt-0">
                            <h1 data-aos="fade-up" className="text-3xl font-bold sm:text-4xl text-center">
                                WELCOME BACK
                            </h1>
                            <p data-aos="fade-up" className="text-sm leading-5 tracking-wide text-gray-800 text-center">
                                Welcome back! Please enter your details
                            </p>
                            <div className="flex flex-col gap-4">
                                <div data-aos="fade-up" className="flex items-center">
                                    <p>Email</p>
                                </div>
                                <input type="email" className="border border-gray-800 rounded-full h-10 px-4" placeholder="Enter your email" onChange={(e) => setEmail(e.target.value)} />

                                <div data-aos="fade-up" className="flex items-center gap-4">
                                    <p>Password</p>
                                </div>
                                <input type="password" className="border border-gray-800 rounded-full h-10 px-4" placeholder="****************" onChange={(e) => setPassword(e.target.value)} />

                                <div className='flex justify-between items-center'>
                                    <div className="flex">
                                        <input type="checkbox" name="" id="" />
                                        <label htmlFor="Remember me">Remember me</label>
                                    </div>
                                    <span>
                                        <Link to=''>Forgot password?</Link>
                                    </span>
                                </div>
                                <button className='w-full mb-1 text-[18px] mt-2 rounded-full bg-red-600 text-gray-50 shadow-lg py-2 ' type="submit">Sign in</button>


                                <div className='text-center'>
                                    <span>
                                        Don't have an account? <Link to='/SignUp' className="text-brand-400 ">Sign up for free</Link>
                                    </span>
                                </div>
                            </div>
                        </div>
                        {/* image section */}
                        <div data-aos="zoom-in">
                            <img
                                src={BannerImg}
                                alt=""
                                className="max-w-[1000px] h-[1000px] w-full object-cover object-left"
                            />
                        </div>

                    </div>

                </div>

            </div>
        </form>
    );
};

export default Login;