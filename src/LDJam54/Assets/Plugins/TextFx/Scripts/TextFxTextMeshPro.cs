#if TMP
using UnityEngine;
using System.Collections;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TextFx
{
	public class TMPTextDataHandler : TextFxAnimationManager.GuiTextDataHandler
	{
		Vector3[] m_posData;
		Color32[] m_colData;
		int m_numBaseLetters;
		int m_extraVertsPerLetter = 0;
		int m_totalVertsPerLetter;
		int m_numExtraQuads = 0;

		public int NumVerts { get { return m_posData.Length; } }
		public int ExtraVertsPerLetter { get { return m_extraVertsPerLetter; } }
		public int NumVertsPerLetter { get { return m_totalVertsPerLetter; } }

		public TMPTextDataHandler(Vector3[] meshVerts, Color32[] meshCols, int numBaseLetterMeshes)
		{
			m_posData = meshVerts;
			m_colData = meshCols;
			m_numBaseLetters = numBaseLetterMeshes;

			m_totalVertsPerLetter = 4; //m_numBaseLetters > 0 ? m_posData.Length / m_numBaseLetters : 0;
		}

		public Vector3[] GetLetterBaseVerts(int letterIndex)
		{
			Vector3[] verts = new Vector3[4];

			for(int idx=0; idx < 4; idx++)
			{
				// Fill in the base mesh verts 
				verts[idx] = m_posData[(letterIndex * 4) + idx];
			}

			return verts;
		}

		static int[] baseColVertIndexes = new int[]{ 2,3,0,1 };

		public Color[] GetLetterBaseCols(int letterIndex)
		{
			Color[] cols = new Color[4];

			for(int idx=0; idx < 4; idx++)
			{
				// Fill in the base mesh verts 
				cols[idx] = m_colData[(letterIndex * 4) + baseColVertIndexes[idx]];
			}

			return cols;
		}

		public Vector2[] GetLetterBaseUVs(int letterIndex)
		{
			return null;
		}

		public Vector3[] GetLetterExtraVerts(int letterIndex)
		{
			Vector3[] extraVerts = null;

			if (m_extraVertsPerLetter > 0)
			{
				extraVerts = new Vector3[m_extraVertsPerLetter];

				for(int quadIdx = 0; quadIdx < m_numExtraQuads; quadIdx++)
				{
					for(int vIdx=0; vIdx < 4; vIdx++)
					{
						// Fill in the extra mesh verts 
						extraVerts[quadIdx * 4 + vIdx] = m_posData[(quadIdx * m_numBaseLetters * 4) + (letterIndex * 4) + vIdx];
					}
				}
			}

			return extraVerts;
		}

		public Color[] GetLetterExtraCols(int letterIndex)
		{
			Color[] extraCols = null;

			if (m_extraVertsPerLetter > 0)
			{
				extraCols = new Color[m_extraVertsPerLetter];

				for(int quadIdx = 0; quadIdx < m_numExtraQuads; quadIdx++)
				{
					for(int vIdx=0; vIdx < 4; vIdx++)
					{
						// Fill in the extra mesh cols 
						extraCols[quadIdx * 4 + vIdx] = m_colData[(quadIdx * m_numBaseLetters * 4) + (letterIndex * 4) + vIdx];
					}
				}
			}

			return extraCols;
		}

		public Vector2[] GetLetterExtraUVs(int letterIndex)
		{
			return null;
		}
	}

	public class TextFxTextMeshPro : TextMeshPro, TextFxAnimationInterface
	{
		// Editor TextFx conversion
#if UNITY_EDITOR
		[MenuItem ("Tools/TextFx/Convert TMP Text to TextFx", false, 200)]
		static void ConvertToTextFX ()
		{
			GameObject activeGO = Selection.activeGameObject;
			TextMeshPro tmpText = activeGO.GetComponent<TextMeshPro> ();
			TextFxTextMeshPro textfxUGUI = activeGO.GetComponent<TextFxTextMeshPro> ();

			if(textfxUGUI != null)
				return;

			GameObject tempObject = new GameObject("temp");
			textfxUGUI = tempObject.AddComponent<TextFxTextMeshPro>();

			TextFxTextMeshPro.CopyComponent(tmpText, textfxUGUI);

			DestroyImmediate (tmpText);

			TextFxTextMeshPro newUGUIEffect = activeGO.AddComponent<TextFxTextMeshPro> ();

			TextFxTextMeshPro.CopyComponent (textfxUGUI, newUGUIEffect);

			DestroyImmediate (tempObject);

			// Forces the mesh to be redrawn, taking into account the material settings.
			newUGUIEffect.Reset ();

			Debug.Log (activeGO.name + "'s TextMeshPro component converted into a TextFxTextMeshPro component");
		}

		[MenuItem ("Tools/TextFx/Convert TMP Text to TextFx", true)]
		static bool ValidateConvertToTextFX ()
		{
			if(Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<TextMeshPro>() != null)
				return true;
			else
				return false;
		}

		static void CopyComponent(TextMeshPro textFrom, TextMeshPro textTo)
		{
			textTo.text = textFrom.text;
			textTo.font = textFrom.font;
			textTo.fontSize = textFrom.fontSize;
			textTo.enableAutoSizing = textFrom.enableAutoSizing;
			textTo.fontStyle = textFrom.fontStyle;
			textTo.lineSpacing = textFrom.lineSpacing;
			textTo.alignment = textFrom.alignment;
			textTo.color = textFrom.color;
			textTo.colorGradient = textFrom.colorGradient;
			textTo.material = textFrom.material;
			textTo.renderer.sharedMaterials = textFrom.renderer.sharedMaterials;
			textTo.enabled = textFrom.enabled;
			textTo.enableWordWrapping = textFrom.enableWordWrapping;
			textTo.wordWrappingRatios = textFrom.wordWrappingRatios;
			textTo.enableKerning = textFrom.enableKerning;
			textTo.extraPadding = textFrom.extraPadding;
			textTo.characterSpacing = textFrom.characterSpacing;
			textTo.paragraphSpacing = textFrom.paragraphSpacing;
			textTo.richText = textFrom.richText;
			textTo.raycastTarget = textFrom.raycastTarget;
			textTo.margin = textFrom.margin;
		}

#endif

		// Animation Interface Properties

		public string AssetNameSuffix { get { return "_TMP"; } }
		public float MovementScale { get { return 1f; } }
		public int LayerOverride { get { return -1; } }          // Renders objects on the UI layer
		public TEXTFX_IMPLEMENTATION TextFxImplementation { get { return TEXTFX_IMPLEMENTATION.TMP; } }
		public TextAlignment TextAlignment {
			get {
				switch (alignment) {
					case TextAlignmentOptions.Baseline:
					case TextAlignmentOptions.Bottom:
					case TextAlignmentOptions.Center:
					case TextAlignmentOptions.Midline:
					case TextAlignmentOptions.Top:
						return TextAlignment.Center;
					case TextAlignmentOptions.BaselineRight:
					case TextAlignmentOptions.BottomRight:
					case TextAlignmentOptions.MidlineRight:
					case TextAlignmentOptions.TopRight:
					case TextAlignmentOptions.Right:
						return TextAlignment.Right;
					default:
						return TextAlignment.Left;
				}
			}
		}
		public bool FlippedMeshVerts { get { return true; } }

		[SerializeField]
		TextFxAnimationManager m_animation_manager;
		public TextFxAnimationManager AnimationManager { get { return m_animation_manager; } }

		[SerializeField]
		GameObject m_gameobject_reference;
		public GameObject GameObject { get { if( m_gameobject_reference == null) m_gameobject_reference = gameObject; return m_gameobject_reference; } }

		public bool CurvePositioningEnabled { get { return true; } }

		[SerializeField]
		bool m_renderToCurve = false;
		public bool RenderToCurve { get { return m_renderToCurve; } set { m_renderToCurve = value; } }

		[SerializeField]
		TextFxBezierCurve m_bezierCurve;
		public TextFxBezierCurve BezierCurve { get { return m_bezierCurve; }
			set
			{
				m_bezierCurve = value;

				// Update the curve data based on current BezierCurve state
				m_animation_manager.CheckCurveData ();

				// Update mesh values to latest using new curve offset values
				ForceUpdateCachedVertData();

				// Update text mesh
				UpdateTextFxMesh();
			}
		}

#if UNITY_EDITOR
		public void DrawBezierInspector()
		{
			m_bezierCurve.OnInspector (RecordUndoObject);
		}

		public void OnSceneGUIBezier(Vector3 position_offset, Vector3 scale)
		{
			m_bezierCurve.OnSceneGUI (position_offset, scale, RecordUndoObject);
		}

		void RecordUndoObject(string label)
		{
			Undo.RecordObject(this, label);
		}
#endif
	public UnityEngine.Object ObjectInstance { get { return this; } }

		public System.Action OnMeshUpdateCall { get; set; }

		// TextFxTextMeshPro specific variables

		[SerializeField]
		Vector3[] m_currentVerts;
		[SerializeField]
		Color32[] m_currentColours;

		bool m_textFxUpdateGeometryCall = false;	// Set to highlight Geometry update calls triggered by TextFx
		bool m_textFxAnimDrawCall = false;		// Denotes whether the subsequent OnFillVBO call has been triggered by TextFx or not

		protected override void OnEnable()
		{
			base.OnEnable ();

			if (m_animation_manager == null)
				m_animation_manager = new TextFxAnimationManager ();

			m_animation_manager.SetParentObjectReferences (gameObject, transform, this);
		}

		protected override void Awake()
		{
			base.Awake ();

			if (m_animation_manager == null)
				m_animation_manager = new TextFxAnimationManager ();

			m_animation_manager.SetParentObjectReferences (gameObject, transform, this);

			if(!Application.isPlaying)
			{
				return;
			}

			m_animation_manager.OnStart ();
		}


		void Update ()
		{
			if(!Application.isPlaying || !m_animation_manager.Playing)
			{
				return;
			}

			m_animation_manager.UpdateAnimation ();

			TextFxMarkAsChanged ();
		}

		public void ForceUpdateCachedVertData()
		{
			m_animation_manager.PopulateDefaultMeshData(true);
		}

		// Interface Method: To redraw the mesh with the provided mesh vertex positions
		public void UpdateTextFxMesh()
		{
			TextFxMarkAsChanged ();
		}

		// Interface Method: To set the text of the text renderer
#if TMP_1_5_1_OR_NEWER
		public void SetText( string new_text )
#else
		new public void SetText(string new_text)
#endif
		{
			text = new_text;

			m_textFxAnimDrawCall = false;
		}

		public void SetColour(Color colour)
		{
			color = colour;

			m_animation_manager.SetDataRebuildCallFrame ();
		}

		// Interface Method: Returns the number of verts used for the current rendered text
		public int NumMeshVerts
		{
			get
			{
				return m_currentVerts != null ? m_currentVerts.Length : 0;
			}
		}

		// Interface Method: Access to a specified vert from the current state of the rendered text
		public Vector3 GetMeshVert(int index)
		{
			return m_currentVerts[index];
		}

		// Interface Method: Access to a specified vert colour from the current state of the rendered text
		public Color GetMeshColour(int index)
		{
			return m_currentColours[index];
		}

		// Wrapper to catch all TextFx related calls to UI.Text's UpdateGeometry.
		void TextFxMarkAsChanged()
		{
			m_textFxUpdateGeometryCall = true;

			if(m_textFxUpdateGeometryCall)
			{
				//				Debug.Log("TextFx Triggering Geometry Update");
				m_textFxAnimDrawCall = true;
			}
			else
			{
				//				Debug.Log("System calling Geometry Update, GUI.changed " + GUI.changed);
				m_textFxAnimDrawCall = false;
			}

			m_textFxUpdateGeometryCall = false;

			GenerateTextMesh ();
		}

		bool _forceNativeDrawCall = false;
		string _strippedText;
		int _numRenderedLetters;
		Vector3[] _vertsToUse;
		Color32[] _colsToUse;

		protected override void GenerateTextMesh()
		{
			// NOTE: TMP can make use of more than one text rendering mesh. In the case where fallback font's are used, SubMeshUI objects are used.
			// Because of this, this method needs to combine together (in order) the verts from each mesh for TFAnimationManager to use.
			// Then parse out the combined animatedVerts array into the separate meshes again for rendering. 


			if( ! m_textFxAnimDrawCall || _forceNativeDrawCall)
			{
				_forceNativeDrawCall = false;

				// Text has changed, need to update mesh verts and re-cache them

				base.GenerateTextMesh ();

				_strippedText = text;

				if(richText)
				{
					// Replace any <sprite> tags with full-stops so that they still get animated as a character
					_strippedText = System.Text.RegularExpressions.Regex.Replace(_strippedText, @"<sprite=[^>]+>", ".");
				
					_strippedText = TextFxHelperMethods.StripRichTextCode (_strippedText);
				}

				_numRenderedLetters = m_textInfo.characterCount - m_textInfo.spaceCount; 

				// Update current mesh vert/colour state
				m_currentVerts = new Vector3[_numRenderedLetters * 4];
				m_currentColours = new Color32[_numRenderedLetters * 4];

				int charIndex = 0;
				int[] meshInfoIndexes = new int[m_textInfo.meshInfo.Length];
				Color[] fontVertColours = {	(m_enableVertexGradient ? m_fontColorGradient.topRight : Color.white) * m_fontColor,
											(m_enableVertexGradient ? m_fontColorGradient.bottomRight : Color.white) * m_fontColor,
											(m_enableVertexGradient ? m_fontColorGradient.bottomLeft : Color.white) * m_fontColor,
											(m_enableVertexGradient ? m_fontColorGradient.topLeft : Color.white) * m_fontColor};

				for(int idx=0; idx < m_textInfo.characterCount; idx++)
				{
					if(!m_textInfo.characterInfo[idx].isVisible)
					{
						// Skip invisible characterInfos
						continue;
					}

					int characterMaterialIndex = m_textInfo.characterInfo[idx].materialReferenceIndex;

					for(int v=0; v < 4; v++)
					{
						m_currentVerts [(charIndex * 4) + v] = m_textInfo.meshInfo [characterMaterialIndex].vertices [ (meshInfoIndexes[characterMaterialIndex] * 4) + v];
						m_currentColours [(charIndex * 4) + v] = fontVertColours[v];
					}

					charIndex++;
					meshInfoIndexes[characterMaterialIndex]++;
				}


				// Call to update animation letter setups
				m_animation_manager.UpdateText(	_strippedText,
												new TMPTextDataHandler(m_currentVerts, m_currentColours, _numRenderedLetters),
												white_space_meshes: false);

				if(m_animation_manager.CheckCurveData())
				{
					// Update mesh values to latest using new curve offset values
					m_animation_manager.PopulateDefaultMeshData(true);

					_vertsToUse = m_animation_manager.MeshVerts;

					// Re-assign latest meshInfo to mesh
					if (_vertsToUse.Length == mesh.vertexCount)
					{
						mesh.vertices = _vertsToUse;

						m_currentVerts = _vertsToUse;
					}
					else if(mesh.vertexCount > _vertsToUse.Length)
					{
						if (m_currentVerts.Length != mesh.vertexCount)
						{
							m_currentVerts = new Vector3[mesh.vertexCount];
						}

						for (int idx = 0; idx < _vertsToUse.Length; idx++)
						{
							m_currentVerts [idx] = _vertsToUse [idx];
						}

						mesh.vertices = m_currentVerts;
					}
				}


				if(Application.isPlaying
					|| m_animation_manager.Playing					// Animation is playing, so will now need to render this new mesh in the current animated state
					#if UNITY_EDITOR
					|| TextFxAnimationManager.ExitingPlayMode		// To stop text being auto rendered in to default position when exiting play mode in editor.
					#endif
				)
				{
					// The verts need to be set to their current animated states
					// So recall OnFill, getting it to populate with whatever available animate mesh data for this frame
					m_textFxAnimDrawCall = true;

					// Update animated mesh values to latest
					m_animation_manager.UpdateMesh(true, true, delta_time: 0);

					GenerateTextMesh();
					return;
				}

			}
			else
			{
				int charIndex = 0;
				int[] meshInfoIndexes = new int[m_textInfo.meshInfo.Length];
				Vector3[] animatedVerts = m_animation_manager.MeshVerts;
				Color[] animatedCols = m_animation_manager.MeshColours;
				Vector3[][] meshVertsToUse = new Vector3[m_textInfo.meshInfo.Length][];
				Color32[][] meshColsToUse = new Color32[m_textInfo.meshInfo.Length][];

				// Initialise size of vert arrays to match mesh sizes
				for(int idx=0; idx < m_textInfo.meshInfo.Length; idx++)
				{
					if(m_textInfo.meshInfo[idx].mesh == null)
					{
						return;
					}

					meshVertsToUse[idx] = new Vector3[m_textInfo.meshInfo[idx].mesh.vertexCount];
					meshColsToUse[idx] = new Color32[m_textInfo.meshInfo[idx].mesh.vertexCount];
				}

				// Assign the animatedVerts back to their original mesh
				for(int idx=0; idx < m_textInfo.characterCount; idx++)
				{
					if(!m_textInfo.characterInfo[idx].isVisible)
					{
						// Skip invisible characterInfos
						continue;
					}

					int characterMaterialIndex = m_textInfo.characterInfo[idx].materialReferenceIndex;

					for(int v=0; v < 4; v++)
					{
						meshVertsToUse[characterMaterialIndex][ (meshInfoIndexes[characterMaterialIndex] * 4) + v ] = animatedVerts[(charIndex * 4) + v];
						meshColsToUse[characterMaterialIndex][ (meshInfoIndexes[characterMaterialIndex] * 4) + v ] = animatedCols[(charIndex * 4) + v];
					}

					charIndex++;
					meshInfoIndexes[characterMaterialIndex]++;
				}

				// Assign the verts and colours back to their respective meshes
				for(int idx=0; idx < m_textInfo.meshInfo.Length; idx++)
				{
					Mesh targetMesh = m_textInfo.meshInfo[idx].mesh;

					targetMesh.vertices = meshVertsToUse[idx];
					targetMesh.colors32 = meshColsToUse[idx];
				}

#if UNITY_EDITOR
				// Set object dirty to trigger sceneview redraw/update. Calling SceneView.RepaintAll() doesn't work for some reason.
				EditorUtility.SetDirty( GameObject );
#endif
			}

			// Reset textFx anim call flag
			m_textFxAnimDrawCall = false;

			_forceNativeDrawCall = false;

			if(OnMeshUpdateCall != null)
				OnMeshUpdateCall();
		}
	}
}
#endif