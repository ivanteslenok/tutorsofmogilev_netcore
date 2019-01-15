import {
  CITY,
  CITYS_URL,
  LOAD_LIST,
  CREATE_ITEM,
  UPDATE_ITEM,
  REMOVE_ITEM
} from '../constants';

export const loadCities = () => ({
  type: CITY + LOAD_LIST,
  get: CITYS_URL
});

export const createCity = city => ({
  type: CITY + CREATE_ITEM,
  payload: { data: city },
  post: CITYS_URL
});

export const updateCity = (id, city) => ({
  type: CITY + UPDATE_ITEM,
  payload: { id, data: city },
  put: CITYS_URL
});

export const removeCity = id => ({
  type: CITY + REMOVE_ITEM,
  payload: { id },
  del: CITYS_URL
});
