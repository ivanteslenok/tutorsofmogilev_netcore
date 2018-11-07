import _ from 'lodash';
import { createSelector } from 'reselect';

const getSubjects = store => store.subjects.items;

export const getSortedSubjects = createSelector(
  [getSubjects],
  (subjects) => _.sortBy(subjects, distr => distr['name'])
);
