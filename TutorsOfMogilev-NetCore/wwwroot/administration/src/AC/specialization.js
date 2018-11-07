import {
  SPECIALIZATION,
  SPECIALIZATIONS_URL,
  LOAD_LIST,
  CREATE_ITEM,
  UPDATE_ITEM,
  REMOVE_ITEM
} from '../constants';

export const loadSpecializations = () => ({
  type: SPECIALIZATION + LOAD_LIST,
  get: SPECIALIZATIONS_URL
});

export const createSpecialization = specialization => ({
  type: SPECIALIZATION + CREATE_ITEM,
  payload: { data: specialization },
  post: SPECIALIZATIONS_URL
});

export const updateSpecialization = (id, specialization) => ({
  type: SPECIALIZATION + UPDATE_ITEM,
  payload: { id, data: specialization },
  put: SPECIALIZATIONS_URL
});

export const removeSpecialization = id => ({
  type: SPECIALIZATION + REMOVE_ITEM,
  payload: { id },
  del: SPECIALIZATIONS_URL
});
