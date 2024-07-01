import React from 'react';

import { ExclamationTriangleIcon } from '@heroicons/react/24/outline';
import { useNavigate } from 'react-router-dom';

const Forbidden = () => {
  const navigate = useNavigate();

  const handleGoHome = () => {
    navigate('/home');
  };

  return (
    <div className="flex flex-col items-center justify-center min-h-screen bg-gray-100 p-4">
      <ExclamationTriangleIcon className="w-16 h-16 text-red-500" />
      <h1 className="mt-4 text-4xl font-bold text-gray-800">403</h1>
      <p className="mt-2 text-lg text-gray-600">Bạn không có quyền truy cập trang này.</p>
      <button 
        className="mt-6"
        color="red"
        onClick={handleGoHome}
      >
        Về trang chủ
      </button>
    </div>
  );
};

export default Forbidden;
