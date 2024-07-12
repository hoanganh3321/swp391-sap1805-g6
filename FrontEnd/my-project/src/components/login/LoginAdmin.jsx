// @ts-nocheck
import React from 'react';
import { Link } from 'react-router-dom'
import BannerImg from "../../assets/img/banner/jewelry.jpg";
import axios from 'axios';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { loginAdmin } from '../../redux/apiRequest';
import { useDispatch } from 'react-redux';

const LoginAdmin = () => {

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
        loginAdmin(newUser, dispatch, navigate);

    };


    return (
        <form onSubmit={handleLogin}>
            <div className="min-h-[550px] flex justify-center items-center py-12 sm:py-0">
                <div className="container">
                    <div className="grid items-center grid-cols-1 gap-6 sm:grid-cols-2">
                        {/* text details section */}
                        <div className="flex flex-col justify-center gap-6 sm:pt-0">
                            <h1 data-aos="fade-up" className="text-3xl font-bold text-center sm:text-4xl">
                                WELCOME BACK Admin
                            </h1>
                            <p data-aos="fade-up" className="text-sm leading-5 tracking-wide text-center text-gray-800">
                                Welcome back! Please enter your details
                            </p>
                            <div className="flex flex-col gap-4">
                                <div data-aos="fade-up" className="flex items-center">
                                    <p>Email</p>
                                </div>
                                <input type="email" className="h-10 px-4 border border-gray-800 rounded-full" placeholder="Enter your email" onChange={(e) => setEmail(e.target.value)} />

                                <div data-aos="fade-up" className="flex items-center gap-4">
                                    <p>Password</p>
                                </div>
                                <input type="password" className="h-10 px-4 border border-gray-800 rounded-full" placeholder="****************" onChange={(e) => setPassword(e.target.value)} />

                                <div className='flex items-center justify-between'>
                                    <div className="flex">
                                        <input type="checkbox" name="" id="" />
                                        <label htmlFor="Remember me" className='ml-3'>Remember me</label>
                                    </div>
                                    <span>
                                        <Link to=''>Forgot password?</Link>
                                    </span>
                                </div>
                                <button className='w-full mb-1 text-[18px] mt-2 rounded-full bg-bloom text-gray-50 shadow-lg py-2 ' type="submit">Sign in</button>

                            </div>
                        </div>
                        {/* image section */}
                        <div data-aos="zoom-in">
                            <img
                                src={BannerImg}
                                alt=""
                                className="max-w-[900px] h-[730px] w-full object-cover object-left"
                            />
                        </div>

                    </div>

                </div>

            </div>
        </form>
    );
};

export default LoginAdmin;