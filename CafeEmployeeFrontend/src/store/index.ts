import { configureStore } from '@reduxjs/toolkit';
import { cafeApi } from '../api/cafeApi';
import { employeeApi } from '../api/employeeApi';

export const store = configureStore({
  reducer: {
    [cafeApi.reducerPath]: cafeApi.reducer,
    [employeeApi.reducerPath]: employeeApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(cafeApi.middleware, employeeApi.middleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
