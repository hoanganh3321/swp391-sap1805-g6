import React from 'react';
import { Link } from 'react-router-dom'
import BannerImg from "../../assets/img/banner/jewelry.jpg";

const Signup = () => {
    return (
        <div className="min-h-[550px] flex justify-center items-center py-12 sm:py-0">
            <div className="container">
                <div className="grid items-center grid-cols-1 gap-6 sm:grid-cols-2">
                    {/* text details section */}
                    <div className="flex flex-col justify-center gap-6 sm:pt-0">
                        <h1 data-aos="fade-up" className="text-3xl font-bold sm:text-4xl text-center">
                            RESISTER
                        </h1>
                        <div className='flex flex-col gap-1'>
                            <div data-aos="fade-up" className="flex items-center">
                                <p>First name</p>
                            </div>
                            <input type="text" className="border border-gray-800 rounded-full h-10 px-4" />


                        </div>
                        <div className='flex flex-col gap-1'>
                            <div data-aos="fade-up" className="flex item-center">
                                <p>Last name</p>
                            </div>
                            <input type="text" className="border border-gray-800 rounded-full h-10 px-4" />
                        </div>

                        <div className="flex flex-col gap-4">
                            <div data-aos="fade-up" className="flex items-center">
                                <p>Email</p>
                            </div>
                            <input type="email" className="border border-gray-800 rounded-full h-10 px-4" placeholder="Enter your email" />

                            <div data-aos="fade-up" className="flex items-center gap-4">
                                <p>Password</p>
                            </div>
                            <input type="password" className="border border-gray-800 rounded-full h-10 px-4" placeholder="   ****************" />


                            <button className='w-full mb-1 text-[18px] mt-2 rounded-full bg-red-600 text-gray-50 shadow-lg py-2 ' type="submit">Create account</button>


                            <div className='text-center'>
                                <span>
                                    Already have an account? <Link to='/Login' className="text-brand-400"> Log in</Link>
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

    );
};

export default Signup;