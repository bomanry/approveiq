import { PathLocationStrategy } from '@angular/common';
import { Directive, Inject, inject, OnDestroy } from "@angular/core";
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from "@angular/router";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";

@Directive()
export abstract class ComponentBase implements OnDestroy {

	protected router = inject(Router);
	protected route = inject(ActivatedRoute);
	protected location = inject(PathLocationStrategy);

  // protected baseService = inject(BaseService);
  // protected studentService = inject(StudentService);
  // protected assignedSessionService = inject(AssignedSessionService);
  // protected auditLogService = inject(AuditLogService);
  // protected sessionService = inject(SessionService);
  // protected sessionTypeService = inject(SessionTypeService);
  // protected assessmentService = inject(AssessmentService);
  // protected externalAssessmentService = inject(ExternalAssessmentService);
  // protected tutorService = inject(TutorService);
  // protected dataImportService = inject(DataImportService);
  // protected districtService = inject(DistrictService);
  // protected assessmentTypeService = inject(AssessmentTypeService);
  // protected schoolService = inject(SchoolService);
  // protected schoolYearService = inject(SchoolYearService);
  // protected disabilityStatusService = inject(DisabilityStatusService);
  // protected ethnicityService = inject(EthnicityService);
  // protected raceService = inject(RaceService);
  // protected roleService = inject(RoleService);
  // protected suffixService = inject(SuffixService);
  // protected b2cUserService = inject(B2CUserService);
  // protected authService =inject(MsalService);
  // protected msalBroadcastService = inject (MsalBroadcastService);
  // protected subjectService = inject(SubjectService);
  // protected subtopicService = inject(SubtopicService);
  // protected tagService = inject(TagService);
  // protected hqMaterialService = inject(HqMaterialService);
  // protected reportingService = inject(ReportingService);
  // protected stateService = inject(StateService);
  // protected resourceService = inject(ResourceService);
  // protected blackoutDateService = inject(BlackoutDateService);
  // protected performanceSummaryService = inject(PerformanceSummaryService);
  //
	// protected loadingService = inject(LoadingService);
	// protected odataService = inject(ODataService);
	// protected toastr = inject(ToastrNotifier);
	// protected unsavedDialogService = inject(UnsavedChangesDialogService);
	// protected confirmationService = inject(ConfirmationService);
	// protected dialogService = inject(DialogService);

  //protected modalService = inject(NgbModal);

	private _destroy$?: Subject<void>;

	protected takeUntilDestroy = <T>() => {
		if (!this._destroy$) this._destroy$ = new Subject<void>();
		return takeUntil<T>(this._destroy$);
	};

	ngOnDestroy(): void {
		if (this._destroy$) {
			this._destroy$.next();
			this._destroy$.complete();
		}
	}

	protected refreshPage() {
		this.router.routeReuseStrategy.shouldReuseRoute = () => false;
		this.router.onSameUrlNavigation = 'reload';
		this.router.navigate(['./'], { relativeTo: this.route });
	}

	protected print() {
		window.print();
	}

	protected canDeactivate(form: FormGroup<any>) {
		let promise = Promise.resolve(true);
    // if (form.dirty)
    //   return this.unsavedDialogService.showUnsavedDialog();
    return promise;
	}
}
