import { configureStore } from '@reduxjs/toolkit';
import { cafeApi } from '../api/cafeApi';
import { employeeApi } from '../api/employeeApi';

const store = configureStore({
  reducer: {
    // Add the reducers for the APIs using their respective reducerPaths
    [cafeApi.reducerPath]: cafeApi.reducer,
    [employeeApi.reducerPath]: employeeApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware()
      .concat(cafeApi.middleware) // Add cafeApi middleware
      .concat(employeeApi.middleware), // Add employeeApi middleware
});

export default store;
