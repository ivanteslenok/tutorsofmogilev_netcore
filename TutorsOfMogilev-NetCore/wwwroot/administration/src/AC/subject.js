import {
  SUBJECT,
  SUBJECTS_URL,
  LOAD_LIST,
  CREATE_ITEM,
  UPDATE_ITEM,
  REMOVE_ITEM
} from '../constants';

export const loadSubjects = () => ({
  type: SUBJECT + LOAD_LIST,
  get: SUBJECTS_URL
});

export const createSubject = subject => ({
  type: SUBJECT + CREATE_ITEM,
  payload: { data: subject },
  post: SUBJECTS_URL
});

export const updateSubject = (id, subject) => ({
  type: SUBJECT + UPDATE_ITEM,
  payload: { id, data: subject },
  put: SUBJECTS_URL
});

export const removeSubject = id => ({
  type: SUBJECT + REMOVE_ITEM,
  payload: { id },
  del: SUBJECTS_URL
});
