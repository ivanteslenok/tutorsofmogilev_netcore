import {
  TUTOR,
  TUTORS_URL,
  TUTORS_SUBJECTS_ADDITIONAL_URL,
  TUTORS_SPECIALIZATIONS_ADDITIONAL_URL,
  LOAD_LIST,
  SAVE_QUERY_PARAMS,
  SET_FILTER,
  CREATE_ITEM,
  UPDATE_ITEM,
  REMOVE_ITEM,
  UPDATE_TUTORS_SUBJECTS,
  UPDATE_TUTORS_SPECIALIZATIONS,
  PHONE,
  PHONES_URL,
  CONTACT,
  CONTACTS_URL
} from '../constants';

export const loadTutors = queryParams => ({
  type: TUTOR + LOAD_LIST,
  get: `${TUTORS_URL}${queryParams || ''}`
});

export const setLastQueryParams = queryParams => ({
  type: TUTOR + SAVE_QUERY_PARAMS,
  payload: { params: queryParams }
});

export const setFilter = filter => ({
  type: TUTOR + SET_FILTER,
  payload: { filter }
});

export const createTutor = tutor => ({
  type: TUTOR + CREATE_ITEM,
  payload: { data: tutor },
  post: TUTORS_URL
});

export const updateTutor = (id, tutor) => ({
  type: TUTOR + UPDATE_ITEM,
  payload: { id, data: tutor },
  put: TUTORS_URL
});

export const removeTutor = id => ({
  type: TUTOR + REMOVE_ITEM,
  payload: { id },
  del: TUTORS_URL
});

export const updateTutorSubjects = (id, data) => ({
  type: TUTOR + UPDATE_TUTORS_SUBJECTS,
  payload: { id, data, additionalUrl: TUTORS_SUBJECTS_ADDITIONAL_URL },
  put: TUTORS_URL
});

export const updateTutorSpecializations = (id, data) => ({
  type: TUTOR + UPDATE_TUTORS_SPECIALIZATIONS,
  payload: { id, data, additionalUrl: TUTORS_SPECIALIZATIONS_ADDITIONAL_URL },
  put: TUTORS_URL
});

export const addPhone = phone => ({
  type: PHONE + CREATE_ITEM,
  payload: { data: phone },
  post: PHONES_URL
});

export const removePhone = id => ({
  type: PHONE + REMOVE_ITEM,
  payload: { id },
  del: PHONES_URL
});

export const addContact = contact => ({
  type: CONTACT + CREATE_ITEM,
  payload: { data: contact },
  post: CONTACTS_URL
});

export const removeContact = id => ({
  type: CONTACT + REMOVE_ITEM,
  payload: { id },
  del: CONTACTS_URL
});
