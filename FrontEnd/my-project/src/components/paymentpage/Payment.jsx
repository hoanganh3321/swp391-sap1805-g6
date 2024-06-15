import React from 'react';
import Footer from '../footer/FooterHomePage';
import Navbar from '../navbar/Navbar';
import { Link } from 'react-router-dom';



const Payment = () => {
    return (
        <div className="duration-200 bg-white dark:bg-gray-900 dark:text-white">
            < Navbar />



            <div className="min-h-[550px] flex justify-center items-center py-12 sm:py-0">
                <div className="container">
                    <div className="grid items-center grid-cols-1 gap-60 sm:grid-cols-2">

                        {/* cart table will be here */}
                        <div className="flex flex-col justify-center gap-6 sm:pt-0 pl-40 pb-20">
                            {/* thêm bảng cart từ đây*/}


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
                                <input type="email" className="border border-gray-800 rounded-full h-10" placeholder="   Enter your email" />

                                <div data-aos="fade-up" className="flex items-center gap-4">
                                    <p>Password</p>
                                </div>
                                <input type="password" className="border border-gray-800 rounded-full h-10" placeholder="   ****************" />

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


                            {/* kết thúc ở đây */}
                        </div>


                        {/* Payment infor */}
                        <div className="flex flex-col justify-center gap-6 sm:pt-0 pr-40 pb-20">
                            <h1 data-aos="fade-up" className="text-3xl font-bold sm:text-4xl text-center">
                                Payment Details
                            </h1>
                            <p data-aos="fade-up" className="text-sm leading-5 tracking-wide text-gray-800 text-center">
                                Complete your purchase by providing the payment details
                            </p>
                            <div className="flex flex-col gap-4">
                                <div data-aos="fade-up" className="flex items-center">
                                    <p>Email address</p>
                                </div>
                                <input type="email" className="border border-gray-800 rounded-lg h-10 px-4" placeholder="Enter your email" />

                                <div data-aos="fade-up" className="flex items-center gap-4">
                                    <p>Card details</p>
                                </div>
                                <input type="text" className="border border-gray-800 rounded-lg h-10 px-4" placeholder="****************" />

                                <div data-aos="fade-up" className="flex items-center gap-4">
                                    <p>Cardholder Name</p>
                                </div>
                                <input type="text" className="border border-gray-800 rounded-lg h-10 px-4" placeholder="Name" />

                                <div data-aos="fade-up" className="flex items-center gap-4">
                                    <p>Billing address</p>
                                </div>
                                <input type="text" className="border border-gray-800 rounded-lg h-10 px-4" placeholder="Address" />



                                <div className="bg-white border border-gray-800 p-4 shadow-md rounded-lg">
                                    <div className="flex justify-between items-center mb-4">
                                        <span className="text-gray-800">Subtotal</span>
                                        <span className="font-semibold">$199.00</span>
                                    </div>
                                    <div className="flex justify-between items-center mb-4">
                                        <span className="text-gray-800">VAT (7%)</span>
                                        <span className="font-semibold">$13.93</span>
                                    </div>
                                    <div className="flex justify-between items-center mb-4">
                                        <span className="text-gray-800">Total</span>
                                        <span className="font-semibold">$206.65</span>
                                    </div>
                                    <button className="w-full bg-blue-600 text-white p-2 rounded-lg mt-4 hover:bg-blue-600">
                                        Pay $206.65
                                    </button>
                                </div>


                            </div>
                        </div>


                    </div>

                </div>

            </div>

            <Footer />
        </div >
    );
};

export default Payment;
