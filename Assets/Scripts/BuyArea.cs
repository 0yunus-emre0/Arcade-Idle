using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BuyArea : MonoBehaviour
{
    [SerializeField] PoolGenerator _poolGenerator;
    [SerializeField] private int _cost;
    [SerializeField] private float _loadTimeAmount;
    [SerializeField] private GameObject _desk;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] CanvasGroup _showPanel;
    [SerializeField] CanvasGroup _loadingPanel;
    [SerializeField] Image _loadingFillImage;

    private void Start() {
        _costText.text = _cost.ToString();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && GameManager.Instance.GameCoin >= _cost){
            BuyDesk();
        }
    }

    void BuyDesk(){
        _showPanel.DOFade(0,.5f);
        _loadingPanel.DOFade(1,.5f);
        _loadingFillImage.DOFillAmount(1,_loadTimeAmount).OnComplete(()=>{
            GameManager.Instance.SetGameCoin(-_cost);
            GameObject spawnvfx = _poolGenerator.GetFromPoolBase(PoolIndexs.VFXBase,PoolIndexs.DeskSpawnVfx);
            spawnvfx.transform.position = _desk.transform.position;
            if(_desk.TryGetComponent<PrepareDesk>(out PrepareDesk desk)){
                int orderIndex = desk._quizType.packIndex;
                GameManager.Instance.SetCustomerCount(1,orderIndex);
            }
            Invoke(nameof(SpawnDesk),.2f);
        });
    }
    void SpawnDesk(){
        _desk.SetActive(true);
        gameObject.SetActive(false);
    }    
}
