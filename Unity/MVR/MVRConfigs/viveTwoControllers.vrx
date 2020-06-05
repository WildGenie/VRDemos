<?xml version="1.0" encoding="UTF-8"?>
<MiddleVR>
	<Kernel LogLevel="2" LogInSimulationFolder="0" EnableCrashHandler="0" Version="1.7.0.8" />
	<DeviceManager>
		<Driver Type="vrDriverDirectInput" />
		<Driver Type="vrDriverOpenVRSDK" TargetCamera="CameraStereo0" ControllerTrackersNb="2" TrackingReferencesNb="2" />
		<Wand Name="Right" Driver="0" Axis="OpenVR.Controller0.Joystick.Axis" HorizontalAxis="0" HorizontalAxisScale="1" VerticalAxis="1" VerticalAxisScale="1" AxisDeadZone="0.3" Buttons="OpenVR.Controller0.Joystick.Buttons" Button0="0" Button1="1" Button2="2" Button3="3" Button4="4" Button5="5" />
		<Wand Name="Left" Driver="0" Axis="OpenVR.Controller1.Joystick.Axis" HorizontalAxis="0" HorizontalAxisScale="1" VerticalAxis="1" VerticalAxisScale="1" AxisDeadZone="0.3" Buttons="OpenVR.Controller1.Joystick.Buttons" Button0="0" Button1="1" Button2="2" Button3="3" Button4="4" Button5="5" />
	</DeviceManager>
	<DisplayManager Fullscreen="0" AlwaysOnTop="0" WindowBorders="0" ShowMouseCursor="1" VSync="0" GraphicsRenderer="2" AntiAliasing="0" ForceHideTaskbar="0" SaveRenderTarget="0" ChangeWorldScale="0" WorldScale="1">
		<Node3D Name="VRSystemCenterNode" Tag="VRSystemCenter" Parent="None" Tracker="0" IsFiltered="0" Filter="0" PositionLocal="0.000000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" />
		<Node3D Name="HandNode1" Tag="Hand1" Parent="VRSystemCenterNode" Tracker="OpenVR.Controller0.Tracker" IsFiltered="0" Filter="0" UseTrackerX="1" UseTrackerY="1" UseTrackerZ="1" UseTrackerYaw="1" UseTrackerPitch="1" UseTrackerRoll="1" />
		<Node3D Name="HeadNode" Tag="Head" Parent="VRSystemCenterNode" Tracker="OpenVR.HMD0.Tracker" IsFiltered="0" Filter="0" UseTrackerX="1" UseTrackerY="1" UseTrackerZ="1" UseTrackerYaw="1" UseTrackerPitch="1" UseTrackerRoll="1" />
		<Camera Name="Camera0" Tag="MainCamera" Parent="HeadNode" Tracker="0" IsFiltered="0" Filter="0" PositionLocal="0.000000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" VerticalFOV="60" Near="0.1" Far="1000" Screen="0" ScreenDistance="1" UseViewportAspectRatio="1" AspectRatio="1.33333" />
		<CameraStereo Name="CameraStereo0" Parent="HeadNode" Tracker="0" IsFiltered="0" Filter="0" PositionLocal="0.000000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" VerticalFOV="60" Near="0.1" Far="1000" Screen="0" ScreenDistance="1" UseViewportAspectRatio="1" AspectRatio="1.33333" InterEyeDistance="0.065" LinkConvergence="1" />
		<Node3D Name="HandNode2" Tag="Hand2" Parent="VRSystemCenterNode" Tracker="OpenVR.Controller1.Tracker" IsFiltered="0" Filter="0" UseTrackerX="1" UseTrackerY="1" UseTrackerZ="1" UseTrackerYaw="1" UseTrackerPitch="1" UseTrackerRoll="1" />
		<Viewport Name="Viewport0" Left="0" Top="0" Width="800" Height="600" Camera="Camera0" Stereo="0" StereoMode="3" CompressSideBySide="0" StereoInvertEyes="0" OculusRiftWarping="0" OffScreen="0" UseHomography="0" />
	</DisplayManager>
	<ClusterManager NVidiaSwapLock="0" DisableVSyncOnServer="1" ForceOpenGLConversion="0" BigBarrier="0" SimulateClusterLag="0" MultiGPUEnabled="0" ImageDistributionMaxPacketSize="8000" ClientConnectionTimeout="60" />
</MiddleVR>
