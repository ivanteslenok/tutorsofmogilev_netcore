import { connect } from 'react-redux';
import * as tutorAC from '../AC/tutor';
import * as cityAC from '../AC/city';
import DataGrid from '../components/DataGrid/index';

const mapStateToProps = state => {
  return {
    columns: state.tutors.gridColumns,
    defaultColumnWidths: state.tutors.gridColumnWidths,
    gridColumnEditing: state.tutors.gridColumnEditing,
    defaultHiddenColumnNames: state.tutors.gridHiddenColumnNames,
    defaultColumnOrder: state.tutors.gridColumnOrder,
    availableColumnsValues: {
      city: state.cities.items
    },
    availableColumnsValuesLoaded: state.cities.loaded,
    defaultValues: {    
      city: state.cities.items[0]
    },
    rows: state.tutors.items,
    totalCount: state.tutors.totalCount,
    loading: state.tutors.loading,
    lastQueryParams: state.tutors.lastQueryParams
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: filter => {
      dispatch(tutorAC.loadTutors(filter));
    },
    loadAvailableValues: () => {
      dispatch(cityAC.loadCities());
    },
    saveQueryParams: queryParams => {
      dispatch(tutorAC.setLastQueryParams(queryParams));
    },
    getDeleteText: deleteRow => {
      if (deleteRow) {
        return `${deleteRow.firstName} ${deleteRow.lastName || ''}`;
      }
      return '';
    },
    remove: id => {
      dispatch(tutorAC.removeTutor(id));
    },
    create: tutor => {
      dispatch(tutorAC.createTutor(tutor));
    },
    update: (id, tutor) => {
      dispatch(tutorAC.updateTutor(id, tutor));
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(DataGrid);
