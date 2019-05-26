import { connect } from 'react-redux';
import { createGridAction } from '../AC/dataGrid';
import * as tutorAC from '../AC/tutor';
import cityAC from '../AC/city';
import DataGrid from '../components/DataGrid/index';

const mapStateToProps = state => {
  return {
    gridColumns: state.tutors.gridColumns,
    gridColumnWidths: state.tutors.gridColumnWidths,
    gridColumnEditing: state.tutors.gridColumnEditing,
    defaultHiddenColumnNames: state.tutors.gridDefaultHiddenColumnNames,
    hiddenColumnNames: state.tutors.gridHiddenColumnNames,
    gridColumnOrder: state.tutors.gridColumnOrder,
    availableColumnsValues: {
      city: state.cities.items
    },
    availableColumnsValuesLoaded: state.cities.loaded,
    defaultValues: {
      city: state.cities.items[0]
    },
    rows: state.tutors.items,
    sorting: state.tutors.sorting,
    filter: state.tutors.filter,
    pageSize: state.tutors.pageSize,
    currentPage: state.tutors.currentPage,
    totalCount: state.tutors.totalCount,
    loading: state.tutors.loading,
    lastQueryParams: state.tutors.lastQueryParams
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: filter => dispatch(tutorAC.loadTutors(filter)),
    loadAvailableValues: () => dispatch(cityAC.loadEntities()),
    saveQueryParams: queryParams =>
      dispatch(tutorAC.setLastQueryParams(queryParams)),
    getDeleteText: deleteRow => {
      if (deleteRow) {
        return `${deleteRow.firstName} ${deleteRow.lastName || ''}`;
      }
      return '';
    },
    remove: id => dispatch(tutorAC.removeTutor(id)),
    create: tutor => dispatch(tutorAC.createTutor(tutor)),
    update: (id, tutor) => dispatch(tutorAC.updateTutor(id, tutor)),
    onSortingChange: sorting => dispatch(createGridAction('sorting', sorting)),
    onCurrentPageChange: currentPage =>
      dispatch(createGridAction('currentPage', currentPage)),
    onPageSizeChange: pageSize =>
      dispatch(createGridAction('pageSize', pageSize)),
    onColumnOrderChange: order =>
      dispatch(createGridAction('gridColumnOrder', order)),
    onColumnWidthsChange: widths =>
      dispatch(createGridAction('gridColumnWidths', widths)),
    onHiddenColumnNamesChange: hiddenColumns =>
      dispatch(createGridAction('gridHiddenColumnNames', hiddenColumns))
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(DataGrid);
