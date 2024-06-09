import React from "react";

const ErrorModal = ({ message, onClose }) => {
  if (!message) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-start justify-center bg-black bg-opacity-50">
      <div className="w-full max-w-sm p-6 mt-3 bg-white border border-blue-500 rounded-lg">
        <h2 className="mb-4 text-xl font-bold text-blue-600">Error</h2>
        <p className="mb-4 text-blue-600">{message}</p>
        <button
          onClick={onClose}
          className="rounded-lg bg-blue-500 px-3 py-2.5 text-center text-sm font-medium text-white hover:bg-blue-700 focus:outline-none focus:ring-4 focus:ring-blue-300"
        >
          Close
        </button>
      </div>
    </div>
  );
};

export default ErrorModal;
