// src/functions/useDeleteData.js
import { useCallback } from "react";
import axios from "api/axios"; // Adjust the import path if necessary
import useAuth from "hook/useAuth";

const useDeleteData = () => {
  const token = useAuth();

  const deleteData = useCallback(
    async (selectedRow, endpoint) => {
      const headers = {
        Authorization: `Bearer ${token}`,
      };

      try {
        await Promise.all(
          selectedRow.map((id) => axios.delete(`${endpoint}/${id}`, { headers }))
        );
        return { success: true };
      } catch (error) {
        console.error("Failed to delete selected rows", error);
        return { success: false, error };
      }
    },
    [token]
  );

  return deleteData;
};

export default useDeleteData;
