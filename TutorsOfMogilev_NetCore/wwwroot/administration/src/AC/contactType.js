import {
  CONTACT_TYPE,
  CONTACT_TYPES_URL,
  LOAD_LIST,
  CREATE_ITEM,
  UPDATE_ITEM,
  REMOVE_ITEM
} from '../constants';

export const loadContactTypes = () => ({
  type: CONTACT_TYPE + LOAD_LIST,
  get: CONTACT_TYPES_URL
});

export const createContactType = contactType => ({
  type: CONTACT_TYPE + CREATE_ITEM,
  payload: { data: contactType },
  post: CONTACT_TYPES_URL
});

export const updateContactType = (id, contactType) => ({
  type: CONTACT_TYPE + UPDATE_ITEM,
  payload: { id, data: contactType },
  put: CONTACT_TYPES_URL
});

export const removeContactType = id => ({
  type: CONTACT_TYPE + REMOVE_ITEM,
  payload: { id },
  del: CONTACT_TYPES_URL
});
