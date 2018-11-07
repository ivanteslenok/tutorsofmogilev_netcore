import {
  DISTRICT,
  DISTRICTS_URL,
  LOAD_LIST,
  CREATE_ITEM,
  UPDATE_ITEM,
  REMOVE_ITEM
} from '../constants';

export const loadDistricts = () => ({
  type: DISTRICT + LOAD_LIST,
  get: DISTRICTS_URL
});

export const createDistrict = district => ({
  type: DISTRICT + CREATE_ITEM,
  payload: { data: district },
  post: DISTRICTS_URL
});

export const updateDistrict = (id, district) => ({
  type: DISTRICT + UPDATE_ITEM,
  payload: { id, data: district },
  put: DISTRICTS_URL
});

export const removeDistrict = id => ({
  type: DISTRICT + REMOVE_ITEM,
  payload: { id },
  del: DISTRICTS_URL
});
