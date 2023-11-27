﻿using Microsoft.JSInterop;

namespace Havit.Blazor.Components.Web.Bootstrap;

/// <summary>
/// Component to render modal dialog as a <see href="https://getbootstrap.com/docs/5.3/components/modal/">Bootstrap Modal</see>.<br />
/// Full documentation and demos: <see href="https://havit.blazor.eu/components/HxModal">https://havit.blazor.eu/components/HxModal</see>
/// </summary>
public partial class HxModal : IAsyncDisposable
{
	/// <summary>
	/// A value that is passed to the <c>data-bs-backdrop</c> data attribute when <see cref="Backdrop"/> is set to <see cref="ModalBackdrop.Static"/>.
	/// </summary>
	private const string StaticBackdropValue = "static";

	/// <summary>
	/// Application-wide defaults for the <see cref="HxGrid{TItem}"/>.
	/// </summary>
	public static ModalSettings Defaults { get; }

	static HxModal()
	{
		Defaults = new ModalSettings()
		{
			Animated = true,
			ShowCloseButton = true,
			CloseOnEscape = true,
			Size = ModalSize.Regular,
			Fullscreen = ModalFullscreen.Disabled,
			Backdrop = ModalBackdrop.True,
			Scrollable = false,
			Centered = false,
		};
	}

	/// <summary>
	/// Returns application-wide defaults for the component.
	/// Enables overriding defaults in descendants (use separate set of defaults).
	/// </summary>
	protected virtual ModalSettings GetDefaults() => Defaults;

	/// <summary>
	/// Set of settings to be applied to the component instance (overrides <see cref="Defaults"/>, overridden by individual parameters).
	/// </summary>
	[Parameter] public ModalSettings Settings { get; set; }

	/// <summary>
	/// For modals that simply appear rather than fade in to view, setting <c>false</c> removes the <c>.fade</c> class from your modal markup.
	/// Default value is <c>true</c>.
	/// </summary>
	[Parameter] public bool? Animated { get; set; }
	protected bool AnimatedEffective => this.Animated ?? this.GetSettings()?.Animated ?? GetDefaults().Animated ?? throw new InvalidOperationException(nameof(Animated) + " default for " + nameof(HxModal) + " has to be set.");

	/// <summary>
	/// Returns optional set of component settings.
	/// </summary>
	/// <remarks>
	/// Similar to <see cref="GetDefaults"/>, enables defining wider <see cref="Settings"/> in components descendants (by returning a derived settings class).
	/// </remarks>
	protected virtual ModalSettings GetSettings() => this.Settings;


	/// <summary>
	/// Title in modal header.
	/// </summary>
	[Parameter] public string Title { get; set; }

	/// <summary>
	/// Header template.
	/// </summary>
	[Parameter] public RenderFragment HeaderTemplate { get; set; }

	/// <summary>
	/// Body template.
	/// </summary>
	[Parameter] public RenderFragment BodyTemplate { get; set; }

	/// <summary>
	/// Footer template.
	/// </summary>
	[Parameter] public RenderFragment FooterTemplate { get; set; }

	/// <summary>
	/// Size of the modal. Default is <see cref="ModalSize.Regular"/>.
	/// </summary>
	[Parameter] public ModalSize? Size { get; set; }
	protected ModalSize SizeEffective => this.Size ?? this.GetSettings()?.Size ?? GetDefaults().Size ?? throw new InvalidOperationException(nameof(Size) + " default for " + nameof(HxModal) + " has to be set.");

	/// <summary>
	/// Fullscreen behavior of the modal. Default is <see cref="ModalFullscreen.Disabled"/>.
	/// </summary>
	[Parameter] public ModalFullscreen? Fullscreen { get; set; }
	protected ModalFullscreen FullscreenEffective => this.Fullscreen ?? this.GetSettings()?.Fullscreen ?? GetDefaults().Fullscreen ?? throw new InvalidOperationException(nameof(Fullscreen) + " default for " + nameof(HxModal) + " has to be set.");

	/// <summary>
	/// Indicates whether the modal shows close button in header.
	/// Default value is <c>true</c>.
	/// </summary>
	[Parameter] public bool? ShowCloseButton { get; set; }
	protected bool ShowCloseButtonEffective => this.ShowCloseButton ?? this.GetSettings()?.ShowCloseButton ?? GetDefaults().ShowCloseButton ?? throw new InvalidOperationException(nameof(ShowCloseButton) + " default for " + nameof(HxModal) + " has to be set.");

	/// <summary>
	/// Close icon to be used in header.
	/// If set to <c>null</c>, Bootstrap default close-button will be used.
	/// </summary>
	[Parameter] public IconBase CloseButtonIcon { get; set; }
	protected IconBase CloseButtonIconEffective => this.CloseButtonIcon ?? this.GetSettings()?.CloseButtonIcon ?? GetDefaults().CloseButtonIcon;

	/// <summary>
	/// Indicates whether the modal closes when escape key is pressed.
	/// Default value is <c>true</c>.
	/// </summary>
	[Parameter] public bool? CloseOnEscape { get; set; }
	protected bool CloseOnEscapeEffective => this.CloseOnEscape ?? this.GetSettings()?.CloseOnEscape ?? GetDefaults().CloseOnEscape ?? throw new InvalidOperationException(nameof(CloseOnEscape) + " default for " + nameof(HxModal) + " has to be set.");

	/// <summary>
	/// Indicates whether to apply a backdrop on body while the modal is open.
	/// If set to <see cref="ModalBackdrop.Static"/>, the modal cannot be closed by clicking on the backdrop.
	/// Default value (from <see cref="Defaults"/>) is <see cref="ModalBackdrop.True"/>.
	/// </summary>
	[Parameter] public ModalBackdrop? Backdrop { get; set; }
	protected ModalBackdrop BackdropEffective => this.Backdrop ?? this.GetSettings()?.Backdrop ?? GetDefaults().Backdrop ?? throw new InvalidOperationException(nameof(Backdrop) + " default for " + nameof(HxModal) + " has to be set.");

	/// <summary>
	/// Allows scrolling the modal body. Default is <c>false</c>.
	/// </summary>
	[Parameter] public bool? Scrollable { get; set; }
	protected bool ScrollableEffective => this.Scrollable ?? this.GetSettings()?.Scrollable ?? GetDefaults().Scrollable ?? throw new InvalidOperationException(nameof(Scrollable) + " default for " + nameof(HxModal) + " has to be set.");

	/// <summary>
	/// Allows vertical centering of the modal.<br/>
	/// Default is <c>false</c> (horizontal only).
	/// </summary>
	[Parameter] public bool? Centered { get; set; }
	protected bool CenteredEffective => this.Centered ?? this.GetSettings()?.Centered ?? GetDefaults().Centered ?? throw new InvalidOperationException(nameof(Centered) + " default for " + nameof(HxModal) + " has to be set.");

	/// <summary>
	/// Additional CSS class for the main element (<c>div.modal</c>).
	/// </summary>
	[Parameter] public string CssClass { get; set; }
	protected string CssClassEffective => this.CssClass ?? this.GetSettings()?.CssClass ?? GetDefaults().CssClass;

	/// <summary>
	/// Additional CSS class for the dialog (<c>div.modal-dialog</c> element).
	/// </summary>
	[Parameter] public string DialogCssClass { get; set; }
	protected string DialogCssClassEffective => this.DialogCssClass ?? this.GetSettings()?.DialogCssClass ?? GetDefaults().DialogCssClass;

	/// <summary>
	/// Additional header CSS class (<c>div.modal-header</c>).
	/// </summary>
	[Parameter] public string HeaderCssClass { get; set; }
	protected string HeaderCssClassEffective => this.HeaderCssClass ?? this.GetSettings()?.HeaderCssClass ?? GetDefaults().HeaderCssClass;

	/// <summary>
	/// Additional body CSS class (<c>div.modal-body</c>).
	/// </summary>
	[Parameter] public string BodyCssClass { get; set; }
	protected string BodyCssClassEffective => this.BodyCssClass ?? this.GetSettings()?.BodyCssClass ?? GetDefaults().BodyCssClass;

	/// <summary>
	/// Additional content CSS class (<c>div.modal-content</c>).
	/// </summary>
	[Parameter] public string ContentCssClass { get; set; }
	protected string ContentCssClassEffective => this.ContentCssClass ?? this.GetSettings()?.ContentCssClass ?? GetDefaults().ContentCssClass;

	/// <summary>
	/// Additional footer CSS class (<c>div.modal-footer</c>).
	/// </summary>
	[Parameter] public string FooterCssClass { get; set; }
	protected string FooterCssClassEffective => this.FooterCssClass ?? this.GetSettings()?.FooterCssClass ?? GetDefaults().FooterCssClass;

	/// <summary>
	/// This event is fired immediately when the hide instance method has been called.<br/>
	/// This can be caused by <see cref="HideAsync"/>, close-button, <kbd>Esc</kbd> key or other interaction.
	/// Hiding can be cancelled by setting <see cref="ModalHidingEventArgs.Cancel"/> = <c>true</c>
	/// </summary>
	/// <remarks>
	/// There is intentionally no <c>virtual InvokeOnHidingAsync()</c> method to override as we want to avoid confusion.
	/// The <code>hide.bs.modal</code> event is subscribed only when <see cref="OnHiding"/> callback is set.
	/// </remarks>
	[Parameter] public EventCallback<ModalHidingEventArgs> OnHiding { get; set; }

	/// <summary>
	/// This event is fired when the modal has finished being hidden from the user (will wait for CSS transitions to complete).<br/>
	/// This can be caused by <see cref="HideAsync"/>, close-button, <kbd>Esc</kbd> key or other interaction.
	/// </summary>
	[Parameter] public EventCallback OnClosed { get; set; }

	/// <summary>
	/// Triggers the <see cref="OnClosed"/> event. Allows interception of the event in derived components.
	/// </summary>
	protected virtual Task InvokeOnClosedAsync() => OnClosed.InvokeAsync();

	/// <summary>
	/// This event is fired when an modal element has been made visible to the user (will wait for CSS transitions to complete).
	/// </summary>
	[Parameter] public EventCallback OnShown { get; set; }

	/// <summary>
	/// Triggers the <see cref="OnShown"/> event. Allows interception of the event in derived components.
	/// </summary>
	protected virtual Task InvokeOnShownAsync() => OnShown.InvokeAsync();


	[Inject] protected IJSRuntime JSRuntime { get; set; }


	private bool opened = false; // indicates whether the modal is open
	private bool shouldOpenModal = false; // indicates whether the modal is going to be opened
	private DotNetObjectReference<HxModal> dotnetObjectReference;
	private ElementReference modalElement;
	private IJSObjectReference jsModule;
	private bool disposed;

	public HxModal()
	{
		dotnetObjectReference = DotNetObjectReference.Create(this);
	}

	/// <summary>
	/// Opens the modal.
	/// </summary>
	public Task ShowAsync()
	{
		opened = true; // mark modal as opened
		shouldOpenModal = true; // mark modal to be shown in OnAfterRender			

		StateHasChanged(); // ensures render modal HTML

		return Task.CompletedTask;
	}

	/// <summary>
	/// Closes the modal.
	/// </summary>
	public async Task HideAsync()
	{
		await jsModule.InvokeVoidAsync("hide", modalElement);
	}

	/// <summary>
	/// Receives notification from JS for <c>hide.bs.modal</c> event.
	/// </summary>
	[JSInvokable("HxModal_HandleModalHide")]
	public async Task<bool> HandleModalHide()
	{
		var eventArgs = new ModalHidingEventArgs();
		await OnHiding.InvokeAsync(eventArgs);
		return eventArgs.Cancel;
	}

	/// <summary>
	/// Receives notification from JS for <c>hidden.bs.modal</c> event.
	/// </summary>
	[JSInvokable("HxModal_HandleModalHidden")]
	public async Task HandleModalHidden()
	{
		opened = false;
		await InvokeOnClosedAsync();
		StateHasChanged(); // ensures re-render to remove dialog from HTML
	}

	/// <summary>
	/// Receives notification from JS for <c>shown.bs.modal</c> event.
	/// </summary>
	[JSInvokable("HxModal_HandleModalShown")]
	public async Task HandleModalShown()
	{
		await InvokeOnShownAsync();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		await base.OnAfterRenderAsync(firstRender);

		if (shouldOpenModal)
		{
			// do not run show in every render
			// the line must be prior to JSRuntime (because BuildRenderTree/OnAfterRender[Async] is called twice; in the bad order of lines the JSRuntime would be also called twice).
			shouldOpenModal = false;

			// Running JS interop is postponed to OnAfterAsync to ensure modalElement is set.
			jsModule ??= await JSRuntime.ImportHavitBlazorBootstrapModuleAsync(nameof(HxModal));
			if (disposed)
			{
				return;
			}
			await jsModule.InvokeVoidAsync("show", modalElement, dotnetObjectReference, this.CloseOnEscapeEffective, OnHiding.HasDelegate);
		}
	}

	/// <summary>
	/// Formats a <see cref="ModalBackdrop"/> for supplying the value via the <c>data-bs-backdrop</c> data attribute.
	/// </summary>
	/// <param name="backdrop"></param>
	/// <returns></returns>
	private string GetBackdropSetupValue(ModalBackdrop backdrop)
	{
		return backdrop switch
		{
			ModalBackdrop.Static => StaticBackdropValue,
			ModalBackdrop.True => "true",
			ModalBackdrop.False => "false",
			_ => throw new InvalidOperationException($"Unknown {nameof(ModalBackdrop)} value {BackdropEffective}")
		};
	}

	protected string GetDialogSizeCssClass()
	{
		return this.SizeEffective switch
		{
			ModalSize.Small => "modal-sm",
			ModalSize.Regular => null,
			ModalSize.Large => "modal-lg",
			ModalSize.ExtraLarge => "modal-xl",
			_ => throw new InvalidOperationException($"Unknown {nameof(ModalSize)} value {this.SizeEffective}.")
		};
	}

	protected string GetDialogFullscreenCssClass()
	{
		return this.FullscreenEffective switch
		{
			ModalFullscreen.Disabled => null,
			ModalFullscreen.Always => "modal-fullscreen",
			ModalFullscreen.SmallDown => "modal-fullscreen-sm-down",
			ModalFullscreen.MediumDown => "modal-fullscreen-md-down",
			ModalFullscreen.LargeDown => "modal-fullscreen-lg-down",
			ModalFullscreen.ExtraLargeDown => "modal-fullscreen-xl-down",
			ModalFullscreen.XxlDown => "modal-fullscreen-xxl-down",
			_ => throw new InvalidOperationException($"Unknown {nameof(ModalFullscreen)} value {this.FullscreenEffective}.")
		};
	}

	protected string GetDialogScrollableCssClass()
	{
		if (this.ScrollableEffective)
		{
			return "modal-dialog-scrollable";
		}
		return null;
	}

	protected string GetDialogCenteredCssClass()
	{
		if (this.CenteredEffective)
		{
			return "modal-dialog-centered";
		}
		return null;
	}

	public async ValueTask DisposeAsync()
	{
		await DisposeAsyncCore();

		//Dispose(disposing: false);
	}

	protected virtual async ValueTask DisposeAsyncCore()
	{
		disposed = true;

		if (jsModule != null)
		{
			// We need to remove backdrop when leaving "page" when HxModal is shown (opened).
			if (opened)
			{
				try
				{
					await jsModule.InvokeVoidAsync("dispose", modalElement);
				}
				catch (JSDisconnectedException)
				{
					// NOOP
				}
				catch (TaskCanceledException)
				{
					// NOOP
				}
			}

			try
			{
				await jsModule.DisposeAsync();
			}
			catch (JSDisconnectedException)
			{
				// NOOP
			}
			catch (TaskCanceledException)
			{
				// NOOP
			}
		}

		dotnetObjectReference.Dispose();
	}
}
