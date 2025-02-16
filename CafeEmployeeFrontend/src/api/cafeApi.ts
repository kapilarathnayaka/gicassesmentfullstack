import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { Cafe } from '../types/cafe';

export const cafeApi = createApi({
  reducerPath: 'cafeApi',
  baseQuery: fetchBaseQuery({ baseUrl: 'http://localhost:5000/api/' }),
  endpoints: (builder) => ({
    getCafes: builder.query<Cafe[], string>({
      query: (location = '') => `cafes?location=${location}`,
    }),
    addCafe: builder.mutation<void, Partial<Cafe>>({
      query: (newCafe) => ({
        url: 'cafe',
        method: 'POST',
        body: newCafe,
      }),
    }),
    deleteCafe: builder.mutation<void, string>({
      query: (id) => ({
        url: `cafe/${id}`,
        method: 'DELETE',
      }),
    }),
  }),
});

export const { useGetCafesQuery, useAddCafeMutation, useDeleteCafeMutation } = cafeApi;
